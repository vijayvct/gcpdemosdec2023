﻿// Copyright 2020 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START storage_upload_file]
// [START storage_stream_file_upload]

using Google.Cloud.Storage.V1;
using System;
using System.IO;

public class UploadFileSample
{
    public void UploadFile(
        string bucketName = "your-unique-bucket-name",
        string localPath = "my-local-path/my-file-name",
        string objectName = "my-file-name")
    {
        var storage = StorageClient.Create();
        using var fileStream = File.OpenRead(localPath);
        storage.UploadObject(bucketName, objectName, null, fileStream);
        Console.WriteLine($"Uploaded {objectName}.");
    }
}

// [END storage_stream_file_upload]
// [END storage_upload_file]
