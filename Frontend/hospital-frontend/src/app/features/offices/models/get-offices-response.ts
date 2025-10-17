import { GetOfficeModel } from "./get-office-model";

export interface GetOfficesResponse {
  result: {
    offices: GetOfficeModel[],
    officesOnPage: number;
    totalPages: number;
  };
}