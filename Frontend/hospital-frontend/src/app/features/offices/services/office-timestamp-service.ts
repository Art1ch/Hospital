import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class OfficeTimestampService {
    private readonly timestampKey = "office_timestamp";
    private readonly timestampTimeoutMs = 10000;

    setTimestamp(timeoutMs: number = this.timestampTimeoutMs) : void {
      const timestamp = Date.now() + timeoutMs;
      localStorage.setItem(this.timestampKey, timestamp.toString());
    }

    needFreshData() : boolean {
      const timestamp = localStorage.getItem(this.timestampKey);

      if (!timestamp) {
        return false;
      }

      const needFreshData = Date.now() < parseInt(timestamp);

      if (!needFreshData){
        this.clearTimestamp();
      }

      return needFreshData;
    }

    private clearTimestamp() : void {
      localStorage.removeItem(this.timestampKey);
    }
}