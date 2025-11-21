namespace part_1
{
    public class connect
    {

        public string connecting()
        {
            // Updated connection string to use SQL Server Express with Windows Authentication
            // Make sure the database exists on the server
            return "Server=LabVM1846780\\SQLEXPRESS01;Database=claiming_system;Trusted_Connection=True;TrustServerCertificate=True;";
        }
    }
}
