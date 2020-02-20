import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Subscription} from 'rxjs';
import {ConfigModel} from '../models/config.model';
import {ConfigService} from '../services/config.service';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

    private apiQuery: Subscription;
    config: ConfigModel = {rssLinks: null, refreshFrequency: null};

    settingsForm: FormGroup = new FormGroup({
        link: new FormControl('', [
            Validators.required,
            Validators.pattern('(http|ftp|https)://[\\w-]+(\\.[\\w-]+)+([\\w.,@?^=%&amp;:/~+#-]*[\\w@?^=%&amp;/~+#-])?')
        ]),
        refreshRate: new FormControl('', [
            Validators.pattern('[0-9]*\.?[0-9]+')
        ])
    });

    constructor(private http: HttpClient,
                @Inject('BASE_URL') private baseUrl,
                private configService: ConfigService) {
    }

    async ngOnInit() {
        this.config = await this.configService.getSettings();
        console.log(this.config);
    }

    delete(index) {
        this.config.rssLinks.splice(index, 1);
        this.updateConfig();
    }

    saveConfig() {
        if (this.config.rssLinks.indexOf(this.settingsForm.value.link) === -1) {
            this.config.rssLinks.push(this.settingsForm.value.link);
        }
        this.config.refreshFrequency = this.settingsForm.value.refreshRate ?
            this.settingsForm.value.refreshRate : this.config.refreshFrequency;
        this.updateConfig();
    }

    updateConfig() {
        this.apiQuery = this.http.put(this.baseUrl + 'api/settings', {
            RssLinks: this.config.rssLinks,
            RefreshFrequency: this.config.refreshFrequency
        }).subscribe(res => {
            console.log(res);
            this.configService.saveSettings(this.config);
        }, error => {
            console.log(error);
        });
    }
}
