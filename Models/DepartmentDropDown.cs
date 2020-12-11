namespace ASPNETCore5Demo.Models
{
    public class DepartmentDropDown
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentVersion
    {
        public int DepartmentId { get; set; }
        public byte[] RowVersion { get; set; }
    }
}