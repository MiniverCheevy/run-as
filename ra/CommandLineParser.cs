using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ra
{
    public class CommandLineParser
    {

       
        internal void ParseAndExecute(string[] args)
        {
            var space = " ";
            var config = RaConfiguration.Current;
            Application app = null;
            UserAccount user = null;
            int appIndex = 1;


            if (args[0].ToLower() != "me")
                user = config.Users.FirstOrDefault(c => c.Key.ToLower() == args[0]);

            if (user == null)
            {
                app = config.Apps.First(c => c.Key.ToLower() == args[0]);
                appIndex = 0;
            }
            
            if (app == null)
                app = config.Apps.First(c => c.Key.ToLower() == args[1]);
            
            var userArgs = new List<string>();

            if (args.Count() > appIndex)
            {
                for (int i = appIndex + 1; i < args.Count(); i++)
                {
                    if (!string.IsNullOrWhiteSpace(args[i]))
                    {
                        userArgs.Add(args[i]);
                    }
                }
            }
            Run(user, app, userArgs.ToArray ());
            PostProcessing(user, app, userArgs.ToArray());


        }

        private void PostProcessing(UserAccount user, Application app, string[] p)
        {
            if (app.Key == "clean")
                Clean(user, app, p);
        }

        private void Clean(UserAccount user, Application app, string[] p)
        {
            var path = p.FirstOrDefault();
            if (path == null)
                return;
            if (path == ".")
                path = Environment.CurrentDirectory;
            CleanDirectory(path);

        }

        private void CleanDirectory(string path)
        {
            try
            {

                if (!Directory.Exists(path))
                    return;
                var foldersToPurge = new string[] { @"\bin", @"\obj", @"\packages" };
                var extensionsToPurge = new string[] { ".user", ".suo" };

                foreach (var dir in Directory.GetDirectories(path))
                {
                    bool purge = false;
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        if (extensionsToPurge.Contains(Path.GetExtension(file)))
                        {
                            IoNic.KillFile(file);
                        }
                    }
                    foreach (var item in foldersToPurge)
                    {
                        if (dir.EndsWith(item))
                        {
                            purge = true;
                        }
                    }
                    if (purge)
                        IoNic.KillDir(dir);
                    else
                    {
                        CleanDirectory(dir);
                    }
                }

            }
            catch { }
            
        }

        //http://social.msdn.microsoft.com/Forums/en-US/winforms/thread/28f84724-af3e-4fa1-bd86-b0d1499eaefa#x_FAQAnswer91

        public static void Run(UserAccount user, Application app, string[] userArgs = null)
        {
            //runas /u:sdoucet@DOE.LA.GOV /netonly "C:\Program Files (x86)\Microsoft SQL Server\100\Tools\Binn\PROFILER.EXE"
            var program = string.Empty;
            var args = app.Arguments;
            if (userArgs != null && userArgs.Length != 0)
            {   
                args = string.Format (app.Arguments ,
                 String.Join(" ", userArgs));
            }


            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo();
            if (user != null)
            {
                program = string.Format("{0} {1}", app.FullPath, args);
                args = string.Format(" /u:{0} /netonly \"{1}\"", user.UserName, program);
                p.StartInfo.FileName = @"c:\windows\system32\runas.exe";
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = true;
            }
            else
            {
                p.StartInfo.FileName = app.FullPath;
                
                if (!string.IsNullOrWhiteSpace(args))
                    p.StartInfo.Arguments = args;

                p.StartInfo.UseShellExecute = true;
            }


            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.CreateNoWindow = false;

            if (app.RequiresAdmin)
                p.StartInfo.Verb = "runas";

            p.Start();
        }

    }
}
