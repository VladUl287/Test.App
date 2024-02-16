import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core";

@Component({
    selector: 'app-chat',
    templateUrl: './company.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CompanyComponent implements OnInit {
    ngOnInit(): void {
        throw new Error("Method not implemented.");
    }
}