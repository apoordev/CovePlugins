using Cove.Server.Plugins;
using Cove.Server;
using System.Text.Json;
using System.Timers;
using Timer = System.Timers.Timer;

// Change the namespace and class name!
namespace ChatNotifier
{
    public class ChatNotifier : CovePlugin
    {
        public List<Timer> timers = new List<Timer>();

        public ChatNotifier(CoveServer server) : base(server) { }

    
        public override void onInit()
        {
            base.onInit();
            load_config();
        }



        public void load_config()
        {
          
            List<PrintEntry> source = new List<PrintEntry>();

            using (StreamReader r = new StreamReader("server_messages.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<PrintEntry>>(json);
            }


            foreach (PrintEntry entry in source)
            {
                var interv = entry.interval.Split(":");
                var message = entry.message;
                var seconds = Double.Parse(interv[2]);
                var minutes = Double.Parse(interv[1]);
                var hours = Double.Parse(interv[0]);
                var time = (seconds + (minutes * 60) + (hours * 3600)) * 1000d;
                var timer = create_timer_with_lambda(time, (a, b) =>
                {
                    SendGlobalChatMessage(message);

                    Log(message);
                });

                timers.Add(timer);
            }


        }

        public class PrintEntry
        {
            public string interval { get; set; }
            public string message { get; set; }
        }

        public Timer create_timer_with_lambda(double del, ElapsedEventHandler lambda)
        {
            Timer timer = new Timer(del);
            timer.Start();
            timer.Elapsed += lambda;
            return timer;
        }
    }
}
