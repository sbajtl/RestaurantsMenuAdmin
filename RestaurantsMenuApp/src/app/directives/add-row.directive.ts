import { Directive, Input, HostListener } from '@angular/core';
import { Table } from 'primeng/table';

@Directive({
    selector: '[pAddRow]',
})
export class AddRowDirective {
    @Input() table!: Table;
    @Input() newRow: any;

    @HostListener('click', ['$event'])
    onClick(event: Event) {
        // if (!this.table.isRowEditing(this.newRow)) {
            // Insert a new row
            this.table.value.unshift(this.newRow);
            // Set the new row in edit mode
            this.table.initRowEdit(this.newRow);

            event.preventDefault();
        // }
    }

}