import { OfficeStatus } from "./office-status";

export interface GetOfficeModel{
    id : string;
    address : string;
    registryPhoneNumber : string;
    status : OfficeStatus
}