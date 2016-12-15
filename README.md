# DittoProcessors

## Installing

Install via nuget ``` install-package Gibe.DittoProcessors ```

## Processors
| Processor | Description |
|:----------|:------------|
|ArchetypeStringList| Output archetype content as ```IEnumerable``` of ```string``` |
|Children| Return the children of ```docttypeAlias``` as ```IEnumerable``` of ```string``` |
|ContentPicker| Return the ```IPublishedContent``` linked to by the content picker |
|DropdownMultiple| Return options as ```IEnumerable``` of ```string``` |
|FilePicker| Return the selected file as ```MediaFileModel``` |
|GetPreValueAsString| |
|Grid| Return the grid as ```GridContentModel``` |
|ImagePicker| Return the selected image as ```MediaImageModel``` |
|LinkPicker| Return the link picker data type data as ```LinkPickerModel``` |
|MetaSEO| Return the Meta SEO data type data as ```MetaModel``` |
|MultiNodeTreePicker| Return the selected nodes as ```IEnumerable``` of ```IPublishedContent``` |
|MultipleImagePicker| Return the selected images as ```IEnumerable``` of ```MediaImageModel``` |
|RelatedLinks| Return related links as ```RelatedLinkModel``` |
|StringIsNotEmpty| Return a ```bool``` check for ```!String.IsNullOrEmpty(string)``` |
|StringToCssClass| Return a ```string``` as a ```string``` without special characaters |
|StringToMd5Hash| Return a ```string``` as MD5 hash ```string``` |
|Tags| Return tags as ```IEnumerable``` of ```string``` |
|Url| Return the ```Url``` of an ```IPublishedContent``` |
|UserPicker| Return the selected user as ```IUser``` |
|VideoPicker| Return the ```Url``` of an ```IPublishedContent``` |

# DittoServices

Install via nuget ``` install-package Gibe.DittoProcessors ```