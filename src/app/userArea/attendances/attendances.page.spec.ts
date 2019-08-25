import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { AttendancesPage } from './attendances.page';

describe('Tab1Page', () => {
  let component: AttendancesPage;
  let fixture: ComponentFixture<AttendancesPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AttendancesPage],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(AttendancesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
