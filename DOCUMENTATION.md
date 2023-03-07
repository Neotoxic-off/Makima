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
| Seasons        | `ObservableCollection<SeriesModel>` |                       |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null,
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
| SeasonsWatched | `ObservableCollection<SeasonModel>` |                       |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null,
  "Splash": null,
  "Seasons": [],
  "SeasonsWatched": []
}
```

### Season

| Element         | Type                                 | Example    |
|-----------------|--------------------------------------|------------|
| ID              | `string`                             | "1000001"  |
| Name            | `string`                             | "Season 1" |
| Episodes        | `ObservableCollection<EpisodeModel>` |            |
| EpisodesWatched | `ObservableCollection<EpisodeModel>` |            |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null,
  "Episodes": [],
  "EpisodesWatched": []
}
```

### Episode

| Element         | Type                                 | Example    |
|-----------------|--------------------------------------|------------|
| ID              | `string`                             | "1100001"  |
| Name            | `string`                             | "1.mkv" |

#### JSON Format
```JSON
{
  "ID": null,
  "Name": null
}
```

