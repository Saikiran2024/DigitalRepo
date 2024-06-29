﻿using DigitalTribe.Helpers;
using DTribe.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
//using static System.Net.WebRequestMethods;

namespace DigitalTribe.Controllers
{
    [Route("api/Storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        //private readonly IStorageService _storageService;

        //public StorageController(IStorageService storageService, IWebHostEnvironment hostingEnvironment)
        //{
        //    _storageService = storageService;
        //    _hostingEnvironment = hostingEnvironment;
        //}

        private readonly IConfiguration _configuration;

        public StorageController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("Upload/{ImageID}")]
        public async Task<IActionResult> uploadImages(string ImageID, IFormFile file)
        {


            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            try
            {
                //var uploadsFolderPath = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
                var uploadsFolderPath = _configuration["FileUploadSettings:ServerPath"]; //use this upload in server


                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var fileExtension = Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolderPath, $"{ImageID}{fileExtension}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileUrl = Path.Combine($"{ImageID}{fileExtension}").Replace("\\", "/");

                return Ok(new { FilePath = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }

        }



        //    double fileLength = Convert.ToDouble(Request.Content.Headers.ContentLength);

        //    #region Check the file size before uploading

        //    if (fileLength > 250000000) // filesize 110MB  110000000 old now 250MB
        //    {
        //        return null;
        //        //return Content(HttpStatusCode.BadRequest, FusionMethods.ReturnJsonResult("123", FusionResponseType.EXCEEDED.ToString(), "File Size exceeded, should be less than 250MB.", null));
        //    }
        //    #endregion

        //    // Retrieve a reference to the container.
        //    CloudBlobContainer blobContainer = await BlobStorageMethods.GetBlobContainerAsync(ConfigurationManager.AppSettings["BlobContainerName"]);

        //    var provider = new AzureStorageMultipartFormDataStreamProvider(blobContainer, iamDetails.TenantID);

        //    #region FILE UPLOAD to BLOB
        //    try
        //    {
        //        // upload the file to blob
        //        await Request.Content.ReadAsMultipartAsync(provider);
        //    }
        //    catch (Exception ex)
        //    {
        //        //return Content(HttpStatusCode.BadRequest, FusionMethods.ReturnJsonResult("123", FusionResponseType.EXCEPTION.ToString(), $"An Error occured. Details : {ex.Message}", null));

        //        return BadRequest($"An Error occured. Details : {ex.Message}");
        //    }
        //    #endregion

        //    //Retrieve the filename that is uploaded
        //    string getBlobFileName = provider.FileData.FirstOrDefault()?.LocalFileName.ToString();

        //    string blobFileName = getBlobFileName.Remove(0, (18 + 6)); // here +6 is to remove "/ROOT/" from the blobfilename

        //    if (string.IsNullOrEmpty(getBlobFileName))
        //    {
        //        return null;
        //        //return Content(HttpStatusCode.BadRequest, FusionMethods.ReturnJsonResult("123", FusionResponseType.NOTSUCCESS.ToString(), "An error has occured while uploading the file. Please try again.", null));
        //    }

        //    var fileDetails = provider.FileData.FirstOrDefault();

        //    string actualFileName = fileDetails.Headers.ContentDisposition.FileName.Replace("\"", ""),
        //        contentType = fileDetails.Headers.ContentType.ToString(),
        //      fileSize = Request.Content.Headers.ContentLength.ToString();

        //    // check for the duplicate file and delete from the blob
        //    string duplicateFile = driveDAObj.CheckForDuplicateFileFOlder(iamDetails.TenantID, locationId, actualFileName);

        //    if (string.Equals(duplicateFile, FusionReturnResult.Exists.ToString()))
        //    {
        //        // deletes the blob if duplicate exists
        //        await BlobStorageMethods.PermanentBlobDeleteAsync(iamDetails.TenantID, blobFileName, ConfigurationManager.AppSettings["BlobContainerName"].ToString());

        //        return Ok(FusionMethods.ReturnJsonResult("123", FusionResponseType.WARNING.ToString(), "Cannot upload the file, already exists", null));
        //    }

        //    locationId = provider.FileData.FirstOrDefault().Headers.ContentDisposition.Name.Replace("\"", "");

        //    #region BGW to save the uploaded file with drive location details to TblDrive
        //    AddFileOrFolder addFileDetails = new AddFileOrFolder
        //    {
        //        FileID = blobFileName,
        //        FileName = actualFileName,
        //        FileType = contentType,
        //        FileSize = fileSize,
        //        Location = locationId,
        //        IsSystemFile = false,
        //        Notes = null
        //    };

        //    await driveDAObj.SaveFileOrFolderDetailsAsync(iamDetails.TenantID, iamDetails.UserID, JObject.FromObject(addFileDetails));

        //    #endregion



        //    //#region update storage-quota usage

        //    //_ = await quotaManageObj.UpdateStorageQuotaAsync(iamDetails.TenantID, fileLength);

        //    //#endregion

        //    //return Ok(FusionMethods.ConvertObjectToJSon(blobFileName, "FileID"));
        //    //return Content(HttpStatusCode.Created, FusionMethods.ConvertObjectToJSon(blobFileName, "FileID", "File uploaded successfully."));

        //    var response = await _UserinfoService.LoginUser(mobileNumber, otp);
        //    return ResponseHandler.Handle(response);
        //}

        /// <summary>
        /// //////////////
        /// </summary>



        private const string BucketName = "your-bucket-name";
        //private readonly IAmazonS3 _s3Client;

        //public StorageController(IAmazonS3 s3Client)
        //{
        //    _s3Client = s3Client;
        //}

        [HttpGet("GetFile/{ImageID}")]
        public async Task<IActionResult> GetImagefromAPI(string ImageID)
        {
            //var uploadsFolderPath = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            var uploadsFolderPath = _configuration["FileUploadSettings:ServerPath"]; //use this upload in server

            var filePath = Path.Combine(uploadsFolderPath, ImageID);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/octet-stream", ImageID);
        }

    }

}