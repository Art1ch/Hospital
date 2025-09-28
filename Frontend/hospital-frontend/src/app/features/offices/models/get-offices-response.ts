import { GetOfficeModel } from "./get-office-model";

export interface GetOfficesResponse {
  result: {
    hasNextPage: boolean,
    offices: GetOfficeModel[]
  }
}