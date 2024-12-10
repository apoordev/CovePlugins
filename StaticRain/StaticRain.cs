﻿using Cove.Server.Plugins;
using Cove.Server;
using Cove.Server.Actor;
using Cove.GodotFormat;

// Change the namespace and class name!
namespace StaticRain
{
    public class StaticRain : CovePlugin
    {
        public StaticRain(CoveServer server) : base(server) { }

        internal RainCloud docksRain;
        internal RainCloud lakeRain;

        public Vector3 docksPosition = new Vector3(151f, 42, 1.5f);
        public Vector3 lakePosition = new Vector3(22.5f, 42, 13);

        public RainCloud spawnRainCloud()
        {
            return ParentServer.spawnRainCloud() as RainCloud;
        }

        public override void onInit()
        {

            Log("Static Rain!");

        }

        public override void onUpdate()
        {
            base.onUpdate();

            if (!Steamworks.SteamAPI.IsSteamRunning())
                return;    

            if (!ParentServer.serverOwnedInstances.Contains(docksRain))
            {
                // the rain cloud has been destroyed / despawned
                docksRain = spawnRainCloud();
                docksRain.isStaic = true;
                docksRain.pos = docksPosition;
            }

            if (!ParentServer.serverOwnedInstances.Contains(lakeRain))
            {
                // the rain cloud has been destroyed / despawned
                lakeRain = spawnRainCloud();
                lakeRain.isStaic = true;
                lakeRain.pos = lakePosition;
            }

        }

        public override void onEnd()
        {
            base.onEnd();

            // remove the actors when the server reloads
            RemoveServerActor(docksRain);
            RemoveServerActor(lakeRain);

        }

    }
}