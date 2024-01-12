using System;

namespace DevFreela.Core.Exceptions
{
    public class ProjectAlteredyStartedException : Exception
    {
        public ProjectAlteredyStartedException() : base("Project is already in Started status")
        {
        }
    }
}
