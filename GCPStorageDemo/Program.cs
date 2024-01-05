using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;

namespace GCPStorageDemo;
class Program
{
    static void Main(string[] args)
    {
        //Creating a bucket
        var storage = StorageClient.Create();
        var bucket = storage.CreateBucket("mygcpproject-407905", "cttestbucket20231474");
        Console.WriteLine(" Bucket Created.");
    }
}
