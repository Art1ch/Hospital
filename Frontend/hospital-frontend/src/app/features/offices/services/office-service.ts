import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GetOfficesResponse } from "../models/get-offices-response";
import { CreateOfficeModel } from "../models/create-office-model";
import { UpdateOfficeModel } from "../models/update-office-model";

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
    return this.httpClient.post<void>(this.apiUrl, office);
  }

  updateOffice(office: UpdateOfficeModel): Observable<void> {
    return this.httpClient.patch<void>(this.apiUrl, office);
  }

  deleteOffice(id: string): Observable<void> {
    const params = new HttpParams().set("id", id);
    return this.httpClient.delete<void>(this.apiUrl, { params });
  }
}
