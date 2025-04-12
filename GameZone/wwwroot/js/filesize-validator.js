$.validator.addMethod('filesize', function (value, element, pram) {
    return this.optional(element) || (element.files[0].size <= pram);
});