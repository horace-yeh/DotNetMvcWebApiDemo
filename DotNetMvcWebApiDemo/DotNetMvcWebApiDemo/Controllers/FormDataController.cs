using DotNetMvcWebApiDemo.Models.APIViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;

namespace DotNetMvcWebApiDemo.Controllers
{
    [RoutePrefix("api/FormData")]
    public class FormDataController : ApiController
    {
        private readonly string _uploadFolderName;
        public FormDataController()
        {
            this._uploadFolderName = "Uploads";
        }

        /// <summary>
        /// Form Post
        /// </summary>
        /// <returns></returns>
        [Route("PostForm")]
        [HttpPost]
        public async Task<CustomReData> PostForm()
        {
            // Ref: https://stackoverflow.com/questions/31168312/multipartmemorystreamprovider-and-reading-user-data-from-multipart-form-data

            if (!this.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            var reData = new CustomReData { Success = true, Message = "", Data = "" };

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await this.Request.Content.ReadAsMultipartAsync(provider);

                var formItems = await this.GetFormItemsAsync(provider.Contents);
                var textContent = formItems.Where(x => !x.isAFileUpload);
                var fileContent = formItems.Where(x => x.isAFileUpload);

                await this.SaveFileAsync(fileContent);
                reData.Data = this.GetTextContentObj(textContent);
                return reData;
            }
            catch(Exception ex)
            {
                reData.Success = false;
                reData.Message = ex.Message;
            }           
            return reData;
        }

        private string GetSaveFolderPath(string folderName)
        {
            var root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            var exists = Directory.Exists(root);
            if (!exists)
            {
                Directory.CreateDirectory(folderName);
            }
            return root;
        }

        private async Task SaveFileAsync(string outputPath,byte[] fileBytes)
        {
            using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                await output.WriteAsync(fileBytes, 0, fileBytes.Length);
            }
        }

        private async Task<List<FormItem>> GetFormItemsAsync(Collection<HttpContent> httpContents)
        {
            var formItems = new List<FormItem>();
            foreach (var item in httpContents)
            {
                var formItem = new FormItem();
                var contentDisposition = item.Headers.ContentDisposition;
                formItem.name = contentDisposition.Name.Trim('"');
                formItem.data = await item.ReadAsByteArrayAsync();
                formItem.fileName = String.IsNullOrEmpty(contentDisposition.FileName) ? "" : contentDisposition.FileName.Trim('"');
                formItem.mediaType = item.Headers.ContentType == null ? "" : String.IsNullOrEmpty(item.Headers.ContentType.MediaType) ? "" : item.Headers.ContentType.MediaType;
                formItems.Add(formItem);
            }
            return formItems;
        }

        private string RandomFileName(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            var guid = Guid.NewGuid().ToString("N");
            return $"{guid}{ext}";
        }

        private object GetTextContentObj(IEnumerable<FormItem> formItems)
        {
            var expo = new ExpandoObject() as IDictionary<string, Object>;
            foreach(var item in formItems)
            {
                expo.Add(item.name, item.value);
            }
            return expo;
        }

        private async Task SaveFileAsync(IEnumerable<FormItem> formItems, bool IsRandomName = false)
        {
            var folderPath = this.GetSaveFolderPath(this._uploadFolderName);
            foreach (var item in formItems)
            {
                var fileName = IsRandomName ? this.RandomFileName(item.fileName) : item.fileName;
                var outputPath = Path.Combine(folderPath, fileName);
                await this.SaveFileAsync(outputPath, item.data);
            }
        }

    }
}
