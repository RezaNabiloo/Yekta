using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BSG.ADInventory.Models;
using Zcf.Data;
using BSG.ADInventory.Services;
using BSG.ADInventory.Entities;
using BSG.ADInventory.Resources;
using Zcf.Models;
using WebMarkupMin.AspNet4.Mvc;

namespace BSG.ADInventory.WebUI.Controllers
{
    [MinifyHtml]
    [Authorize]
    public class FileController : Controller
    {
        private IUnitOfWork unitOfWork;
        
        private readonly IUserService userService;

        public FileController(IUnitOfWork unitOfWork, IUserService userService)
        {
            this.unitOfWork = unitOfWork;            
            this.userService = userService;
        }


        public string err = "";

        #region Actions
        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UploadFile(long id, string fileTitle, string entityType)
        {
            HttpPostedFileBase myFile = Request.Files["MyFile"];
            bool isUploaded = false;
            string message = "";
            long FileId = 0;
            string FileName = "";
            string uploadFolder = "";

            switch (entityType)
            {
                case "Project": uploadFolder = "ProjectDocuments/";
                    break;
                default:
                    break;
            }

            #region Check UpLoaded File         
            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Uploads/"+uploadFolder + id.ToString());

                #region Check for file exists
                if (!System.IO.File.Exists(Path.Combine(pathForSaving, myFile.FileName)))
                {
                    #region  Create Folder if not Exists
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        #region Save File
                        
                        try
                        {

                            myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));

                            switch (entityType)
                            {
                                case "Project":
                                    //ProjectDocument pd = new ProjectDocument { ProjectId = id, Title = fileTitle, FileName=myFile.FileName, SavePath = Path.Combine(pathForSaving, myFile.FileName)};
                                    //this.projectDocumentService.Add(pd);
                                    //FileId = pd.Id;
                                    break;
                                default:
                                    break;
                            }


                            

                            isUploaded = true;
                            message = "";// Resource.UploadFileSuccessfully;

                        }
                        catch (Exception ex)
                        {
                            message = string.Format("خطا در آپلود فایل : {0}", ex.Message);
                            
                        }
                        #endregion
                    }
                    #endregion

                }
                else
                {
                    message = "";// Resource.DuplicateUploadFileName;
                }
                #endregion
            }
            else {
                message = "";// Resource.UploadedFileContainError;
            }
            #endregion

            return Json(new { isUploaded = isUploaded, message = message, fileId = FileId, fileName = FileName }, "text/html");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the folder if needed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    /*TODO: You must process this exception.*/
                    err = "امکان ایجاد دایرکتوری وجود ندارد.";
                    result = false;
                }
            }
            return result;
        }

        #endregion


        #region Delete File
        [HttpPost]
        public virtual ActionResult DeleteFile(long id, string entityType)
        {
            
            bool isDeleted = false;
            string message = "";// Resource.MessageErrorInDeleteFile;
            
            switch (entityType)
            {
                //case "Project":
                //    var deleteFileP = this.projectDocumentService.GetItemByKey(id);
                //    if (deleteFileP != null)
                //    {
                        
                //        if (deleteFileP.CreateUserId == this.userService.GetCurrentUser().Id)
                //        {

                //            if (System.IO.File.Exists(deleteFileP.SavePath))
                //            {
                //                try
                //                {
                //                    System.IO.File.Delete(deleteFileP.SavePath);
                //                    this.projectDocumentService.Remove(deleteFileP);
                //                    isDeleted = true;
                //                    message = Resource.MessageDeleteFileSuccessfull;
                //                }
                //                catch (Exception)
                //                {
                //                    message = Resource.MessageErrorInDeleteFile;

                //                }

                //            }
                //            else
                //                message = Resource.MessageFileNotFound;
                //        }
                //        else {
                //            message = Resource.MessageYouDontHavePermitionToDeleteFile;
                //        }
                //    }
                    
                //    break;
                //case "Job":
                //    var deleteFileJ = this.jobDocService.GetItemByKey(id);
                //    if (deleteFileJ != null)
                //    {
                //        if (deleteFileJ.CreateUserId == this.userService.GetCurrentUser().Id)
                //        {
                //            if (System.IO.File.Exists(deleteFileJ.SavePath))
                //            {
                //                try
                //                {
                //                    System.IO.File.Delete(deleteFileJ.SavePath);
                //                    this.jobDocService.Remove(deleteFileJ);
                //                    isDeleted = true;
                //                    message = Resource.MessageDeleteFileSuccessfull;
                //                }
                //                catch (Exception)
                //                {
                //                    message = Resource.MessageErrorInDeleteFile;

                //                }

                //            }
                //            else
                //                message = Resource.MessageFileNotFound;
                //        }
                //        else
                //        {
                //            message = Resource.MessageYouDontHavePermitionToDeleteFile;
                //        }

                //    }
                //    break;
                default:
                    break;
            }
            
            return Json(new { isDeleted = isDeleted, message = message}, "text/html");
        }
        #endregion

        #region  DownLoad File
        public ActionResult DownloadFile(long? id, string entityType)
        {

            string filePath =string.Empty;
            string fileName = string.Empty;

            switch (entityType)
            {
                //case "Project":
                //    var pDoc = this.projectDocumentService.GetItemByKey(id);
                //    if (pDoc!=null)
                //    {
                //        filePath = pDoc.SavePath;
                //        fileName = pDoc.FileName;
                //    }
                    
                //    break;

                //case "Job":
                //    var jDoc = this.jobDocService.GetItemByKey(id);
                //    if (jDoc != null)
                //    {
                //        filePath = jDoc.SavePath;
                //        fileName = jDoc.FileName;
                //    }
                //    break;
                default:
                    break;
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);                        
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion

        #region Webcam
        [HttpPost]
        public ActionResult SaveCapture(string imageData)
        {
            CustomResult<bool> data = new CustomResult<bool>();
            try
            {
                string fileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                //string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

                //Convert Base64 Encoded string to Byte Array.
                byte[] imageBytes = Convert.FromBase64String(imageData.Split(',')[1]);

                //Save the Byte Array as Image File.
                string filePath = Server.MapPath(string.Format("~/Uploads/PeoplePic/{0}.jpg", fileName));
                System.IO.File.WriteAllBytes(filePath, imageBytes);

                data.Result = true;
                data.ErrorMessage = string.Format("/Uploads/PeoplePic/{0}.jpg", fileName );
            }
            catch (Exception e)
            {   //throw;
                data.Result = false;
                data.ErrorMessage= "خطایی زخ داده. جزئیات :"+e.Message;
            }

            return Json(data);
            
        }
        
        #endregion WebCam
    }
}