function CKEditor_Load() {
    if (arguments.callee.done) return;

    arguments.callee.done = true;


    CKEDITOR.replaceAll(function (textarea, config) {

        config.extraPlugins = 'syntaxhighlight,bbcodeselector';
        //	      config.toolbar_Full = [
        //                                 ['Source'],
        //		                         ['Cut', 'Copy', 'Paste'], ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
        //								 ['-', 'NumberedList', 'BulletedList'],
        //								 ['-', 'Link', 'Unlink', 'Image'],
        //		                         ['Blockquote', 'syntaxhighlight', 'bbcodeselector'],
        //		                         ['SelectAll', 'RemoveFormat'],
        //								 ['About'],
        //								 '/',
        //								 ['Bold', 'Italic', 'Underline', '-', 'TextColor', 'Font', 'FontSize'],
        //								 ['JustifyLeft', 'JustifyCenter', 'JustifyRight']
        //								] ;

        config.toolbar_Full =
  [
      { name: 'document', items: ['Source', '-', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
      { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
      { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
      {
          name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton',

             'HiddenField']
      },
      '/',
      { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
      { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'CreateDiv', '- ', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl'] },
      { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
       ['Blockquote', 'syntaxhighlight', 'bbcodeselector'],
      { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
      '/',
      { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
      { name: 'colors', items: ['TextColor', 'BGColor'] },
      { name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
  ];

        config.entities_greek = false;
        config.entities_latin = false;
        config.allowedContent = true;
    });
};

if (document.addEventListener) {
    document.addEventListener("DOMContentLoaded", CKEditor_Load, false);
}

window.onload = CKEditor_Load;