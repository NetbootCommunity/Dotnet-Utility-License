using System.Xml.Linq;

namespace Netboot.Utility.License;

/// <summary>
/// The customer of a <see cref="License"/>.
/// </summary>
public class Customer : LicenseAttributes
{
    internal Customer(XElement xmlData) : base(xmlData, "CustomerData")
    { }

    /// <summary>
    /// Gets or sets the Name of this <see cref="Customer"/>.
    /// </summary>
    public string Name
    {
        get => GetTag("Name");
        set => SetTag("Name", value);
    }

    /// <summary>
    /// Gets or sets the Company of this <see cref="Customer"/>.
    /// </summary>
    public string Company
    {
        get => GetTag("Company");
        set => SetTag("Company", value);
    }

    /// <summary>
    /// Gets or sets the Email of this <see cref="Customer"/>.
    /// </summary>
    public string Email
    {
        get => GetTag("Email");
        set => SetTag("Email", value);
    }
}