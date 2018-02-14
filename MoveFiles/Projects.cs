using System;
using System.Configuration;
using System.IO;
using System.Linq;


namespace MoveFiles
{
    class Projects
    {
        public string DepotFolder
        {
            get { return ConfigurationManager.AppSettings["DepotPath"]; }
        }

        public string ProjectsFiles
        {
            get { return ConfigurationManager.AppSettings["ProjectsFiles"]; }
        }

        public string DestinationsMachines
        {
            get { return ConfigurationManager.AppSettings["DestinationsMachines"]; }
        }

        public void RunAndMove()
        {
            var projectsToMove = ProjectsFiles.Split(',');
            var destinations = DestinationsMachines.Split(',');

            foreach (var item in destinations)
            {
                foreach (var project in projectsToMove)
                {
                    FindProjects(project, item);
                }
            }
        }

        private void FindProjects(string folder, string destination)
        {
            string wantedDll = String.Empty;
            var projectPath = Path.Combine(DepotFolder, folder);

            if (Directory.Exists(projectPath))
            {
                wantedDll = Directory.GetFiles(projectPath, "*", SearchOption.AllDirectories).First(t => t.Contains(String.Format("{0}.dll", folder)));
            }

            MoveFiles(wantedDll, String.Format(@"\\{0}", destination));
        }

        private void MoveFiles(string pathToFile, string destinationMacine)
        {
            var dest = Path.Combine(destinationMacine, ConfigurationManager.AppSettings["GatorScriptMachineLocation"]);
            var p = pathToFile.Split(new string[] { "GatorScripts" }, StringSplitOptions.RemoveEmptyEntries)[1].Split('\\');
            foreach (var item in p)
            {
                dest = Path.Combine(dest, item);
            }

            try
            {
                File.Copy(pathToFile, dest, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("File not moved on {0}, because of following error", dest));
                Console.WriteLine(ex.Message);
            }
        }
    }
}
