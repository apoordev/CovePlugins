using Cove.Server.Plugins;
using Cove.Server;
using Cove.Server.Chalk;
using System.Text.Json;

// Change the namespace and class name!
namespace PersistentChalk
{
    public class PersistentChalk : CovePlugin
    {
        public PersistentChalk(CoveServer server) : base(server) { }
        private string currentDir = Directory.GetCurrentDirectory();

        private const string ChalkFile = "chalk.json";

        public override void onInit()
        {
            base.onInit();

            // check if there is a chalk.json file in the current directory
            if (File.Exists(Path.Combine(currentDir, ChalkFile)))
            {
                byte[] chalkData = File.ReadAllBytes(Path.Combine(currentDir, ChalkFile));
                Log("Chalk data file found. Loading chalk data...");
                loadChalk(chalkData);
            } else
            {
                // log that the chalk data file does not exist
                Log("Cannot find chalk data file.");
            }
        }

        public long lastUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // now
        public bool hadOfflineUpdate = false;
        public override void onUpdate()
        {
            base.onUpdate();

            if (ParentServer.AllPlayers.Count > 0)
                // At least 1 player is online, reset hadOfflineUpdate
                hadOfflineUpdate = false;

            // Only auto save the chalk data every 5 minutes if a player is online
            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - lastUpdate <= 300)
                return;

            lastUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (ParentServer.AllPlayers.Count == 0 && !hadOfflineUpdate)
                // We're doing a final update, mark as such
                hadOfflineUpdate = true;
            else
                return;

            saveChalk();
            Log("Saving Chalk Data!");
        }

        public void loadChalk(byte[] chalkData)
        {
            // deserialize the chalk data
            List<ChalkCanvas> chalk = JsonSerializer.Deserialize<List<ChalkCanvas>>(chalkData);
            if (chalk != null)
            {
                // set the chalk data to the server's chalk data
                ParentServer.chalkCanvas = chalk;
                Log("Restored Chalk Data");
            } else
            {
                Log("Failed to restore chalk data, chalk file is corrupt");
            }
        }

        public void saveChalk()
        {
            // get the canvas data
            List<ChalkCanvas> chalkData = ParentServer.chalkCanvas;

            // use the json formatter to serialize the chalk data
            string json = JsonSerializer.Serialize(chalkData);

            // write the json string to a file
            File.WriteAllText(Path.Combine(currentDir, ChalkFile), json);
        }
    }
}
