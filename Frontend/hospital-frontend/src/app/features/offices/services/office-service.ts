import { HttpClient, HttpHandler, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GetOfficesResponse } from "../models/get-offices-response";
import { CreateOfficeModel } from "../models/create-office-model";
import { UpdateOfficeModel } from "../models/update-office-model";
import { OfficeStatus } from "../models/office-status";
import { DeleteOfficeModel } from "../models/delete-office-model";
import { OfficeTimestampService } from "./office-timestamp-service";

@Injectable({
  providedIn: 'root'
})
export class OfficeService {
  private readonly apiUrl = 'http://localhost:8000/office';

  constructor(
    private httpClient: HttpClient,
    private timestampService: OfficeTimestampService,
  ) {}

  getOffices(page: number, pageSize: number): Observable<GetOfficesResponse> {
    const httpParams = new HttpParams()
      .set("page", page)
      .set("pageSize", pageSize);
    
    const needFreshData = this.timestampService.needFreshData();
    const headers = new HttpHeaders()
      .set("X-Need-Fresh-Data", needFreshData.toString());

    return this.httpClient.get<GetOfficesResponse>(
      this.apiUrl, { params: httpParams, headers: headers }
    );
  }

  createOffice(office: CreateOfficeModel): Observable<void> {
    const formData = new FormData();

    formData.append("address", office.address);
    formData.append("registryPhoneNumber", office.registryPhoneNumber);
    formData.append("status", OfficeStatus[office.status]);
    formData.append("image", office.image!);

    console.log(formData);

    const observer = this.httpClient.post<void>(this.apiUrl, formData);
    this.timestampService.setTimestamp();

    return observer;
  }

  updateOffice(office: UpdateOfficeModel): Observable<void> {
    const formData = new FormData();

    formData.append("id", office.id);
    formData.append("address", office.address);
    formData.append("registryPhoneNumber", office.registryPhoneNumber);
    formData.append("status", OfficeStatus[office.status]);
    if (office.image instanceof File) {
      formData.append("image", office.image);
    }
  
    if (office.imageUrl) {
      formData.append("imageUrl", office.imageUrl);
    }

    console.log(formData);

    const observer = this.httpClient.patch<void>(this.apiUrl, formData);
    this.timestampService.setTimestamp();

    return observer;
  }

  deleteOffice(office: DeleteOfficeModel): Observable<void> {
    const deleteBody = {
      id: office.id,
      imageUrl: office.imageUrl ? office.imageUrl : "" 
    }
    const observer = this.httpClient.delete<void>(this.apiUrl, {body: deleteBody});
    this.timestampService.setTimestamp();

    return observer;
  }
}
