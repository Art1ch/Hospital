import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumToLabel'
})
export class EnumToLabelPipe implements PipeTransform {
  transform(value: unknown, enumType: object): string {
    if (value === null || value === undefined || !enumType) return '';

    const entries = Object.entries(enumType);
    for (const [key, enumValue] of entries) {
      if (enumValue === value) {
        return key.toLowerCase();
      }
    }

    return String(value).toLowerCase();
  }
}
