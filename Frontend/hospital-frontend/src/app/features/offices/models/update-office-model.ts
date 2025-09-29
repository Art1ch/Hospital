import { OfficeStatus } from "./office-status";

export interface UpdateOfficeModel{
    id : string;
    address : string;
    registryPhoneNumber : string;
    status : OfficeStatus
}