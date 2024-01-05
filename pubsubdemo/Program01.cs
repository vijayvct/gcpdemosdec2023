using Google.Cloud.PubSub.V1;

namespace pubsubdemo
{
    public class Program01
    {
        public static async Task<int> PublishMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts)
        {
            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            int publishedMessageCount = 0;

            var publishTasks = messageTexts.Select(async text =>
            {
                try
                {
                    string message = await publisher.PublishAsync(text);
                    Console.WriteLine($"Published message {message}");
                    Interlocked.Increment(ref publishedMessageCount);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"An error ocurred when publishing message {text}: {exception.Message}");
                }
            });
            await Task.WhenAll(publishTasks);
            return publishedMessageCount;
        }

        public static async Task<int> PullMessagesAsync(string projectId, string subscriptionId, bool acknowledge)
        {
            SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(projectId, subscriptionId);

            SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);
            // SubscriberClient runs your message handle function on multiple
            // threads to maximize throughput.
            int messageCount = 0;
            Task startTask = subscriber.StartAsync((PubsubMessage message, CancellationToken cancel) =>
            {
                string text = System.Text.Encoding.UTF8.GetString(message.Data.ToArray());
                Console.WriteLine($"Message {message.MessageId}: {text}");
                Interlocked.Increment(ref messageCount);
                return Task.FromResult(acknowledge ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack);
            });
            // Run for 5 seconds.
            await Task.Delay(5000);
            //await subscriber.StopAsync(CancellationToken.None);
            // Lets make sure that the start task finished successfully after the call to stop.
            await startTask;
            return messageCount;
        }

        static void Main(string[] args)
        {
            List<string> messages = new List<string>()
            {
                "Message1","Message2","Message3","Message4"
            };

            //PublishMessagesAsync("flash-aviary-409909", "mynewtesttopic", messages).GetAwaiter();

            var result = PullMessagesAsync("flash-aviary-409909", "mysub1", true).GetAwaiter().GetResult();

            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
