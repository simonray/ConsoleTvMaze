using System.Collections.Generic;
using System;

public record Image
{
    public string medium { get; set; }
    public string original { get; set; }
}

public record Self
{
    public string href { get; set; }
}

public record Links
{
    public Self self { get; set; }
    public Previousepisode previousepisode { get; set; }
    public Nextepisode nextepisode { get; set; }
}

public record Schedule
{
    public string time { get; set; }
    public List<string> days { get; set; }
}

public record Rating
{
    public double? average { get; set; }
}

public record Country
{
    public string name { get; set; }
    public string code { get; set; }
    public string timezone { get; set; }
}

public record Network
{
    public int id { get; set; }
    public string name { get; set; }
    public Country country { get; set; }
}

public record WebChannel
{
    public int id { get; set; }
    public string name { get; set; }
    public Country country { get; set; }
}

public record DvdCountry
{
    public string name { get; set; }
    public string code { get; set; }
    public string timezone { get; set; }
}

public record Externals
{
    public int? tvrage { get; set; }
    public int? thetvdb { get; set; }
    public string imdb { get; set; }
}

public record Previousepisode
{
    public string href { get; set; }
}

public record Nextepisode
{
    public string href { get; set; }
}

public record Show
{
    public int id { get; set; }
    public string url { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string language { get; set; }
    public List<string> genres { get; set; }
    public string status { get; set; }
    public int? runtime { get; set; }
    public int? averageRuntime { get; set; }
    public string premiered { get; set; }
    public string officialSite { get; set; }
    public Schedule schedule { get; set; }
    public Rating rating { get; set; }
    public int weight { get; set; }
    public Network network { get; set; }
    public WebChannel webChannel { get; set; }
    public DvdCountry dvdCountry { get; set; }
    public Externals externals { get; set; }
    public Image image { get; set; }
    public string summary { get; set; }
    public int updated { get; set; }
    public Links _links { get; set; }
}

public record Embedded
{
    public Show show { get; set; }
}

public record Root
{
    public int id { get; set; }
    public string url { get; set; }
    public string name { get; set; }
    public int season { get; set; }
    public int? number { get; set; }
    public string type { get; set; }
    public string airdate { get; set; }
    public string airtime { get; set; }
    public DateTime airstamp { get; set; }
    public int? runtime { get; set; }
    public Image image { get; set; }
    public string summary { get; set; }
    public Links _links { get; set; }
    public Embedded _embedded { get; set; }
}

