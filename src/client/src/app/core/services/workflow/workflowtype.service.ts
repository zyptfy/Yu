
import { Injectable, Injector } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient } from '@angular/common/http';
import { UriConstant } from '../../constants/uri-constant';

@Injectable({
    providedIn: 'root'
})

export class WorkFlowTypeService extends BaseService {

	// 构造函数
	constructor(private injector: Injector,
		private http: HttpClient) { super(injector); }

	// 取得数据
	get() {
	  const uri = UriConstant.WorkFlowTypeUri;
	  return this.SafeRequest(this.http.get(uri));
	}

	// 添加数据
	add(data) {
	  const uri = UriConstant.WorkFlowTypeUri;
	  return this.SafeRequest(this.http.post(uri, data));
	}

	// 更新数据
	update(data) {
	  const uri = UriConstant.WorkFlowTypeUri;
	  return this.SafeRequest(this.http.put(uri, data));
	}

	// 删除数据
	delete(id) {
	  const uri = UriConstant.WorkFlowTypeUri + `?id=${id}`;
	  return this.SafeRequest(this.http.delete(uri));
	}
}

