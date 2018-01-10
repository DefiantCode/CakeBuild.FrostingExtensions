using Cake.Core.IO;
using Cake.Incubator;

namespace DefiantCode.Cake.Frosting.Utilities
{
    public class Project
    {
        public DirectoryPath ProjectRoot { get; set; }
        public string ProjectName { get; set; }
        public FilePath ProjectPath { get; set; }
        public CustomProjectParserResult ProjectParserResult { get; set; }
        public DirectoryPath PublishedOutputDirectory { get; set; }

        public Project(FilePath projectPath, CustomProjectParserResult projectParserResult)
        {
            ProjectPath = projectPath;
            ProjectParserResult = projectParserResult;
            ProjectRoot = ProjectPath.GetDirectory();
            ProjectName = ProjectRoot.GetDirectoryName();
        }
    } 
}
