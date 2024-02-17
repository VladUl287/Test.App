import { ChangeDetectionStrategy, Component } from "@angular/core"

@Component({
    selector: 'app-chat',
    templateUrl: './company.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CompanyComponent { }