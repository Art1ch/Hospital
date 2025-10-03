import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GetOfficesResponse } from "../models/get-offices-response";
import { CreateOfficeModel } from "../models/create-office-model";
import { UpdateOfficeModel } from "../models/update-office-model";
import { OfficeStatus } from "../models/office-status";
import { DeleteOfficeModel } from "../models/delete-office-model";

@Injectable({
  providedIn: 'root'
})
export class OfficeService {
  private apiUrl = 'http://localhost:8000/office';

  constructor(private httpClient: HttpClient) {}

  getOffices(page: number, pageSize: number): Observable<GetOfficesResponse> {
    const httpParams = new HttpParams()
      .set("page", page)
      .set("pageSize", pageSize);

    return this.httpClient.get<GetOfficesResponse>(this.apiUrl, { params: httpParams });
  }

  createOffice(office: CreateOfficeModel): Observable<void> {
    const formData = new FormData();

    formData.append("address", office.address);
    formData.append("registryPhoneNumber", office.registryPhoneNumber);
    formData.append("status", OfficeStatus[office.status]);
    formData.append("image", office.image!);

    console.log(formData);

    return this.httpClient.post<void>(this.apiUrl, formData);
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

    return this.httpClient.patch<void>(this.apiUrl, formData);
  }

  deleteOffice(office: DeleteOfficeModel): Observable<void> {
    const deleteBody = {
      id: office.id,
      imageUrl: office.imageUrl ? office.imageUrl : "" 
    }
    return this.httpClient.delete<void>(this.apiUrl, {body: deleteBody});
  }
}
