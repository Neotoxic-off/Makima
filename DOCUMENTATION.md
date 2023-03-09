<p align = "center">
  <a>
    <img src="https://raw.githubusercontent.com/Neotoxic-off/Makima/main/Assets/Logo.png" height="25%" width="25%"/>
    <div align = "center">
        <i>Makima's documentation</i>
    </div>
  </a>
</p>

## Models
### Database

| Element        | Type                                | Example               |
|----------------|-------------------------------------|-----------------------|
| ID             | `string`                            | "0000001"             |
| Path           | `string`                            | "E:\\Series"          |
| SelectedSeries | `SeriesModel`                       |                       |
| Series         | `ObservableCollection<SeriesModel>` |                       |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null,
  "SelectedSeries": null,
  "Series": []
}
```


### Series

| Element        | Type                                | Example               |
|----------------|-------------------------------------|-----------------------|
| ID             | `string`                            | "0000001"             |
| Name           | `string`                            | "Chainsawman"         |
| Splash         | `ImageSource`                       | "https://t.com/i.jpg" |
| Seasons        | `ObservableCollection<SeasonModel>` |                       |
| LatestSeason   | `string`                            |                       |
| LatestEpisode  | `string`                            |                       |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null,
  "Splash": null,
  "Seasons": [],
  "LatestSeason": null,
  "LatestEpisode": null
}
```

### Season

| Element         | Type                                 | Example    |
|-----------------|--------------------------------------|------------|
| ID              | `string`                             | "1000001"  |
| Name            | `string`                             | "Season 1" |
| Episodes        | `ObservableCollection<EpisodeModel>` |            |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null,
  "Episodes": []
}
```

### Episode

| Element         | Type                                 | Example    |
|-----------------|--------------------------------------|------------|
| ID              | `string`                             | "1100001"  |
| Name            | `string`                             | "1.mkv"    |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null
}
```

