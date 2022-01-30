import { Injectable } from "@angular/core";
import * as FileSaver from "file-saver";
@Injectable({
  providedIn: "root"
})
export class FileDownloadService {
  isDownload: boolean = false;
  downloadCount: number = 0;
  constructor() { }
  public DownloadPdfFileUsingBinary(fileData: any, filename: string) {
    var byteString = atob(fileData);
    var ab = new ArrayBuffer(byteString.length);
    var ia = new Uint8Array(ab);
    for (var i = 0; i < byteString.length; i++) {
      ia[i] = byteString.charCodeAt(i);
    }
    var extenstion = "pdf";
    let blobFormat = new Blob([ab], { type: "application/" + extenstion });
    //if (extenstion == "pdf") {
      // if (window.navigator && window.navigator.msSaveOrOpenBlob) {//IE
      //   window.navigator.msSaveOrOpenBlob(blobFormat, filename + ".pdf");
      // }
      // else
        FileSaver.saveAs(blobFormat, filename + ".pdf");
    // }
    // else {
    //   FileSaver.saveAs(blobFormat, filename);
    // }
  }
}
