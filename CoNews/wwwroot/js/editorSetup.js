tinymce.init({
    selector: 'textarea#editor',
    height: 500,
    theme: 'modern',
    plugins: 'print preview fullpage searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount contextmenu colorpicker textpattern help',
    toolbar1: 'fontselect formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat',
    image_advtab: true,
});


tinymce.on('AddEditor', function (e) {
    e.editor.on('change', function (e) {
        editorUpdate();
    }).on('keyup', function (e) {
        editorUpdate();
    })
})

var editArea = $('.mce-edit-area');

//SignalR
const editorConnection = new signalR.HubConnectionBuilder()
    .withUrl("/editorhub")
    .build();

editorConnection.start()
    .catch(err => console.error(err));

editorConnection.on("ReceiveText", (text) => {

    tinymce.activeEditor.setContent(text);

    //Placing the cursor at the end
    tinymce.activeEditor.focus();
    tinymce.activeEditor.selection.select(tinymce.activeEditor.getBody(), true);
    tinymce.activeEditor.selection.collapse(false);
});

function editorUpdate() {
    let content = tinymce.activeEditor.getContent({ format: 'raw' });
    editorConnection.invoke("BroadcastText", content).catch(err => console.error(err));
}
