namespace FirstWebApi.Database;

public class WastePoint
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
}

public class ExternalWastePointModel
{
    public int Id { get; set; }
    public string[] Types { get; set; }
    public Coordinates Coordinates { get; set; }
    public string Name { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string NormalizedPhoneNumber { get; set; }
    public string WebSiteUrl { get; set; }
    public string RpoId { get; set; }
    public string Upi { get; set; }
    public object ContractorName { get; set; }
    public object ContractorAddress { get; set; }
    public bool? isRepontAutomated { get; set; }
}

public class Coordinates
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}


