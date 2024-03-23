
public class Employee
{
    public string Id{get; set; } = string.Empty;
    public string EmployeeName {get; set; } = string.Empty;
    public string StarTimeUtc {get; set; } = string.Empty;
    public string EndTimeUtc {get; set;} = string.Empty;
    public string EntryNotes {get; set;} = string.Empty;
    public object DeletedOn {get; set;} = null;
}
