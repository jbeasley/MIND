/* 
 * attachment
 *
 * This module defines the YANG model for CE Attachment to a Provider Domain PE. The model code is derived from the Nova VPN architecture. This model intends to provide a device-agnostic service API towards northbound systems and therefore abstracts the details of how various types of PE attachment are configured on the network.
 *
 * OpenAPI spec version: 1.0.0.1
 * Contact: jonathan.beasley@refinitiv.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */


using NUnit.Framework;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Model;
using IO.NovaAttSwagger.Client;
using System.Reflection;
using Newtonsoft.Json;

namespace IO.NovaAttSwagger.Test
{
    /// <summary>
    ///  Class for testing DataPutPatch
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the model.
    /// </remarks>
    [TestFixture]
    public class DataPutPatchTests
    {
        // TODO uncomment below to declare an instance variable for DataPutPatch
        //private DataPutPatch instance;

        /// <summary>
        /// Setup before each test
        /// </summary>
        [SetUp]
        public void Init()
        {
            // TODO uncomment below to create an instance of DataPutPatch
            //instance = new DataPutPatch();
        }

        /// <summary>
        /// Clean up after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of DataPutPatch
        /// </summary>
        [Test]
        public void DataPutPatchInstanceTest()
        {
            // TODO uncomment below to test "IsInstanceOfType" DataPutPatch
            //Assert.IsInstanceOfType<DataPutPatch> (instance, "variable 'instance' is a DataPutPatch");
        }


        /// <summary>
        /// Test the property 'IetfRestconfdata'
        /// </summary>
        [Test]
        public void IetfRestconfdataTest()
        {
            // TODO unit test for the property 'IetfRestconfdata'
        }

    }

}
