using Avalonia.Platform;

namespace AvaloniaWebView.TinyMCE;

internal static class HtmlPageBuilder
{
    public static string Build()
    {
        using var scriptStream = AssetLoader.Open(new Uri($"avares://{nameof(AvaloniaWebView)}.{nameof(TinyMCE)}/tiny_mce.min.js"));
        using var scriptStreamReader = new StreamReader(scriptStream);
        var tinyMceScript = scriptStreamReader.ReadToEnd();
        
        using var styleStream = AssetLoader.Open(new Uri($"avares://{nameof(AvaloniaWebView)}.{nameof(TinyMCE)}/tiny_mce.lightgray.css"));
        using var styleStreamReader = new StreamReader(styleStream);
        var tinyMceStyle = styleStreamReader.ReadToEnd();

        var initScript = """
function sendPayload(json) {
    var obj = JSON.parse(json);
    console.log("received payload", obj);
    if (obj.type === 'textChanging')
        tinymce.get("mytextarea").setContent(obj.body);
}
tinymce.init({
    selector: '#mytextarea',
    paste_data_images: true,
    skin: false,
    setup: function(ed){
         ed.on('Paste Change input Undo Redo', function(e){
            var obj = {
                'type': 'textChanged',
                'body': ed.getContent()
            };
            console.log("sending payload", obj);
            invokeCSharpAction(JSON.stringify(obj));
         });
    }
});
""";
        
        return $"""
<!DOCTYPE html>
<html lang="en">
    <head>
        <title>TinyMCE</title>
        <meta http-equiv="content-type" content="text/html; charset=utf-8"/>
        <meta http-equiv="X-UA-Compatible" content="IE=11" />

        <script type="text/javascript">{tinyMceScript}</script>
        <style>{tinyMceStyle}</style>
    </head>
    <body>
        <textarea id="mytextarea"></textarea>
        <script type="text/javascript">{initScript}</script>
    </body>
</html>
""";
    }
}
