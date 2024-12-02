using Cove.Server.Plugins;
using Cove.Server;
using Cove.Server.Actor;
using Cove.GodotFormat;

// Change the namespace and class name!
namespace StaticRain
{
    public class Plugin : CovePlugin
    {
        public Plugin(CoveServer server) : base(server) { }

        internal RainCloud docksRain;
        internal RainCloud lakeRain;

        public Vector3 docksPosition = new Vector3(151f, 42, 1.5f);
        public Vector3 lakePosition = new Vector3(22.5, 42, 13);

        public override void onInit()
        {

            docksRain = ParentServer.spawnRainCloud();
            docksRain.isStaic = true;
            docksRain.pos = docksPosition;

            lakeRain = ParentServer.spawnRainCloud();
            lakeRain.isStaic = true;
            lakeRain.pos = lakePosition;

        }

        public override void onUpdate()
        {
            base.onUpdate();

            if (!ParentServer.serverOwnedInstances.Contains(docksRain))
            {
                // the rain cloud has been destroyed / despawned
                docksRain = ParentServer.spawnRainCloud();
                docksRain.isStaic = true;
                docksRain.pos = docksPosition;
            }

            if (!ParentServer.serverOwnedInstances.Contains(lakeRain))
            {
                // the rain cloud has been destroyed / despawned
                lakeRain = ParentServer.spawnRainCloud();
                lakeRain.isStaic = true;
                lakeRain.pos = lakePosition;
            }

        }

    }
}