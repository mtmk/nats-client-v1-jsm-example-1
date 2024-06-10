using System.Diagnostics;
using NATS.Client;
using NATS.Client.JetStream;

var connection = new ConnectionFactory().CreateConnection("nats://127.0.0.1:4222");
var jsm = connection.CreateJetStreamManagementContext();

// nats s create s1 --subjects s1 --defaults
var info = jsm.GetStreamInfo("s1");

var newConfig = new StreamConfiguration.StreamConfigurationBuilder(info.Config)
    .WithMaxAge(300_000) // ms
    .WithReplicas(2)
    .Build();

jsm.UpdateStream(newConfig);


// nats s info s1
//     Maximum Age: 5m0s

Console.WriteLine(FileVersionInfo.GetVersionInfo(typeof(ConnectionFactory).Assembly.Location).FileVersion);
