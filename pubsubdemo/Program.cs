using Google.Cloud.PubSub.V1;
using Grpc.Core;

namespace pubsubdemo;
class Program
{
    //Creating a Topic 
    static void Main(string[] args)
    {
        PublisherServiceApiClient publisher=PublisherServiceApiClient.Create();

        var topicName= TopicName.FromProjectTopic("flash-aviary-409909","mynewtesttopic");

        Topic topic= null;

        try
        {
            topic=publisher.CreateTopic(topicName);
            Console.WriteLine($"Topic {topic.Name} created");
        }
        catch(RpcException ex) when (ex.Status.StatusCode==StatusCode.AlreadyExists)
        {
            Console.WriteLine($"Topic {topicName} already exists");
        }
    }
}
