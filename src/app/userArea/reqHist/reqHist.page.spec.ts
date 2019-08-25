import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { ReqHistPage } from './reqHist.page';

describe('Tab3Page', () => {
  let component: ReqHistPage;
  let fixture: ComponentFixture<ReqHistPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReqHistPage],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(ReqHistPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
