namespace ra.Messages
{
    public class RunProcessRequest
    {
        public string UserName { get; set; }
        public string ProgramPath { get; set; }
        public string Arguments { get; set; }
        public string UserArgumnets { get; set; }
        public bool RequiresAdmin { get; set; }
    }
}