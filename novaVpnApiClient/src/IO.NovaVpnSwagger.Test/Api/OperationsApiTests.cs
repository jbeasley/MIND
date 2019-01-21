/* 
 * vpn
 *
 * This module defines the YANG model for Nova IPv4 VPN services. The model can be used to create a swagger API definition using the yanger tool.Written by Jon Beasley - WAN Architecture and Strategy - 2018
 *
 * OpenAPI spec version: 1.0.0.1
 * Contact: jonathan.beasley@refinitiv.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using IO.NovaVpnSwagger.Client;
using IO.NovaVpnSwagger.Api;
using IO.NovaVpnSwagger.Model;

namespace IO.NovaVpnSwagger.Test
{
    /// <summary>
    ///  Class for testing OperationsApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class OperationsApiTests
    {
        private OperationsApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new OperationsApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of OperationsApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' OperationsApi
            //Assert.IsInstanceOfType(typeof(OperationsApi), instance, "instance is a OperationsApi");
        }

        
        /// <summary>
        /// Test OperationsGet
        /// </summary>
        [Test]
        public void OperationsGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.OperationsGet();
            //Assert.IsInstanceOf<Operations> (response, "response is Operations");
        }
        
    }

}
