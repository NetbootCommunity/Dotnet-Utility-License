using System;
using System.Xml.Linq;

namespace Netboot.Utility.License;

/// <summary>
/// The product of a <see cref="License"/>.
/// </summary>
public class Product : LicenseAttributes
{
    internal Product(XElement xmlData) : base(xmlData, "ProductData")
    { }

    /// <summary>
    /// Gets or sets the Identifier of this <see cref="Product"/>.
    /// </summary>
    public Guid Id
    {
        get => Guid.Parse(GetTag("Id"));
        set => SetTag("Id", value.ToString());
    }

    /// <summary>
    /// Gets or sets the Name of this <see cref="Product"/>.
    /// </summary>
    public string Name
    {
        get => GetTag("Name");
        set => SetTag("Name", value);
    }
}