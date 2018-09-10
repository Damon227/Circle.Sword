// ***********************************************************************
// Solution         : Damon.Core
// Project          : WebApi
// File             : ValuesController.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly AzureStorageProvider _azureStorageProvider;

        public ValuesController(AzureStorageProvider azureStorageProvider)
        {
            _azureStorageProvider = azureStorageProvider;
        }

        [HttpGet("download")]
        public IActionResult Download()
        {
            //string path = "/public/TestPublish/fengniaoguanjia/蜂鸟管家测试版 Setup 5.4.0.exe";
            string path = @"C:\Users\yuanchengman\Downloads\蜂鸟管家测试版 Setup 5.1.2.exe";

            //Stream stream = await _azureStorageProvider.GetResourceStreamAsync(path);

            FileStream stream = System.IO.File.OpenRead(path);

            //return File(stream, MimeTypeMap.GetMimeType(Path.GetExtension(path)), Path.GetFileName(path));
            return File(stream, "application/octet-stream", "蜂鸟管家测试版 Setup 5.1.2.exe");
        }

        [HttpPost, Route("Login/{userName}")]
        public async Task<IActionResult> Login(string userName, [FromBody] string password)
        {
            DiscoveryResponse dis = await DiscoveryClient.GetAsync("http://localhost:5000");

            //请求令牌
            TokenClient tokenClient = new TokenClient(dis.TokenEndpoint, "Circle.Sword.ClientId.Password", "secret");
            TokenResponse tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(userName, password, "Circle.Sword.Scope");

            return Ok(tokenResponse);
        }

        // GET api/values
        [HttpGet]
        [Authorize(Policy = "Check")]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}