using System.IO;

namespace ExRandomTests {
    public static class Workspace {
        public static string OutDir { private set; get; }

        static Workspace() {
            OutDir = "../../../test_outputs/";

            Directory.CreateDirectory(OutDir);
        }
    }
}
