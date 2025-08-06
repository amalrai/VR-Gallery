<template>
  <a-card :bordered="false">
    <!-- 查询区域 -->
    <div class="table-page-search-wrapper">
      <a-form layout="inline" @keyup.enter.native="searchQuery">
        <a-row :gutter="24">
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('会员权限')">
              <a-select :placeholder="$t('选择会员权限查询')" v-model="queryParam.isAuth" >
                <a-select-option value="">{{$t('请选择')}}</a-select-option>
                <a-select-option value="0">{{$t('关闭')}}</a-select-option>
                <a-select-option value="1">{{$t('开启')}}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('状态')">
              <a-select :placeholder="$t('选择状态查询')" v-model="queryParam.status" >
                <a-select-option value="">{{$t('请选择')}}</a-select-option>
                <a-select-option value="0">{{$t('禁用')}}</a-select-option>
                <a-select-option value="1">{{$t('启用')}}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>

          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('昵称')">
              <j-input :placeholder="$t('输入昵称模糊查询')" v-model="queryParam.nickname"></j-input>
            </a-form-item>
          </a-col>



            <a-col :md="6" :sm="8">
              <a-form-item :label="$t('手机号')">
                <j-input :placeholder="$t('请输入手机号模糊查询')" v-model="queryParam.phone"></j-input>
              </a-form-item>
            </a-col>

            <a-col :md="6" :sm="8">
              <a-form-item :label="$t('邮箱')">
                <j-input :placeholder="$t('请输入邮箱模糊查询')" v-model="queryParam.email"></j-input>
              </a-form-item>
            </a-col>

          <a-col :md="6" :sm="8">
                      <span style="float: left;overflow: hidden;" class="table-page-search-submitButtons">
                        <a-button type="primary" @click="searchQuery" icon="search">{{$t('查询')}}</a-button>
                        <a-button type="primary" @click="searchReset" icon="reload" style="margin-left: 8px">{{$t('重置')}}</a-button>
                      </span>
          </a-col>


        </a-row>
      </a-form>
    </div>
    <!-- 查询区域-END -->

    <!-- 操作按钮区域 -->
    <div class="table-operator">
<!--      <a-button @click="handleAdd" type="primary" icon="plus">新增</a-button>-->
<!--      <a-button @click="batchOpen" type="primary" >{{$t('开启')}}</a-button>-->
<!--      <a-button @click="batchClose" type="primary" >{{$t('关闭')}}</a-button>-->
      <a-button @click="batchEnable" type="primary" >{{$t('启用')}}</a-button>
      <a-button @click="batchDisable" type="primary" >{{$t('禁用')}}</a-button>
      <a-button type="primary" icon="download" @click="handleExportXls($t('用户管理'))">{{$t('导出')}}</a-button>
      <a-button @click="handleVisitorModal" type="primary" >{{$t('游客管理')}}</a-button>
<!--      <a-upload name="file" :showUploadList="false" :multiple="false" :headers="tokenHeader" :action="importExcelUrl" @change="handleImportExcel">-->
<!--        <a-button type="primary" icon="import">导入</a-button>-->
<!--      </a-upload>-->
      <!-- 高级查询区域 -->
<!--      <j-super-query :fieldList="superFieldList" ref="superQueryModal" @handleSuperQuery="handleSuperQuery"></j-super-query>-->
      <a-dropdown v-if="selectedRowKeys.length > 0">
        <a-menu slot="overlay">
          <a-menu-item key="1" @click="batchDel"><a-icon type="delete"/>{{$t('删除')}}</a-menu-item>
        </a-menu>
        <a-button style="margin-left: 8px"> {{$t('批量操作')}} <a-icon type="down" /></a-button>
      </a-dropdown>
    </div>

    <!-- table区域-begin -->
    <div>
      <div class="ant-alert ant-alert-info" style="margin-bottom: 16px;">
        <i class="anticon anticon-info-circle ant-alert-icon"></i> {{$t('已选择')}} <a style="font-weight: 600">{{ selectedRowKeys.length }}</a>{{$t('项')}}
        <a style="margin-left: 24px" @click="onClearSelected">{{$t('清空')}}</a>
      </div>

      <a-table
        ref="table"
        size="middle"
        :scroll="{x:true}"
        bordered
        rowKey="id"
        :columns="columns"
        :dataSource="dataSource"
        :pagination="ipagination"
        :loading="loading"
        :rowSelection="{selectedRowKeys: selectedRowKeys, onChange: onSelectChange}"
        class="j-table-force-nowrap"
        @change="handleTableChange">

        <template slot="htmlSlot" slot-scope="text">
          <div v-html="text"></div>
        </template>
        <template slot="imgSlot" slot-scope="text">
          <span v-if="!text" style="font-size: 12px;font-style: italic;">无图片</span>
          <img v-else :src="getImgView(text)" height="25px" alt="" style="max-width:80px;font-size: 12px;font-style: italic;"/>
        </template>
        <template slot="fileSlot" slot-scope="text">
          <span v-if="!text" style="font-size: 12px;font-style: italic;">无文件</span>
          <a-button
            v-else
            :ghost="true"
            type="primary"
            icon="download"
            size="small"
            @click="downloadFile(text)">
            下载
          </a-button>
        </template>

        <span slot="action" slot-scope="text, record">
           <a @click="handleAuth(record)">{{$t('权限')}}</a>

          <a-divider type="vertical" />

          <a @click="handleDetail(record)">{{$t('详情')}}</a>

          <a-divider type="vertical" />

          <a-popconfirm :title="$t('确定删除吗?')" :okText="$t('确定')" :cancelText="$t('取消')" @confirm="() => handleDelete(record.id)">
                  <a>{{$t('删除')}}</a>
          </a-popconfirm>
        </span>
        <template slot="avatarslot" slot-scope="text, record, index">
          <div class="anty-img-wrap">
            <a-avatar shape="square" :src="getAvatarView(record.avatar)" icon="user"/>
          </div>
        </template>
        <template slot="customRenderYears" slot-scope="years">
          <a-tag v-if="years==1" >{{$t('10代')}}</a-tag>
          <a-tag v-if="years==2" >{{$t('20代')}}</a-tag>
          <a-tag v-if="years==3" >{{$t('30代')}}</a-tag>
          <a-tag v-if="years==4" >{{$t('40代')}}</a-tag>
          <a-tag v-if="years==5" >{{$t('50代')}}</a-tag>
          <a-tag v-if="years==6" >{{$t('60代')}}</a-tag>
          <a-tag v-if="years==7" >{{$t('70代～')}}</a-tag>
        </template>
        <template slot="customRenderSex" slot-scope="sex">
          <a-tag v-if="sex==0" color="blue">{{$t('男')}}</a-tag>
          <a-tag v-if="sex==1" color="pink">{{$t('女')}}</a-tag>
          <a-tag v-if="sex==2" color="gray">{{$t('其他')}}</a-tag>
        </template>
        <template slot="customRenderStatus" slot-scope="status">
          <a-tag v-if="status==0" color="red">{{$t('禁用')}}</a-tag>
          <a-tag v-if="status==1" color="green">{{$t('启用')}}</a-tag>
        </template>
        <template slot="customRenderAuth" slot-scope="isAuth">
          <a-tag v-if="isAuth==0" color="orange">{{$t('关闭')}}</a-tag>
          <a-tag v-if="isAuth==1" color="green">{{$t('开启')}}</a-tag>
        </template>
      </a-table>
    </div>

    <show-user-modal ref="modalForm" @ok="modalFormOk"></show-user-modal>
    <show-visitor-modal ref="visitorModal" @ok=""></show-visitor-modal>
    <show-user-vip-list-modal ref="authForm" @ok=""></show-user-vip-list-modal>
  </a-card>
</template>

<script>

  import '@assets/less/TableExpand.less'
  import { mixinDevice } from '@/utils/mixin'
  import { JeecgListMixin } from '@/mixins/JeecgListMixin'
  import ShowUserModal from './modules/ShowUserModal'
  import ShowVisitorModal from './modules/ShowVisitorModal'
  import ShowUserVipListModal from './ShowUserVipListModal'
  import {getFileAccessHttpUrl, httpAction} from "@api/manage";

  export default {
    name: 'ShowUserList',
    mixins:[JeecgListMixin, mixinDevice],
    components: {
      ShowUserVipListModal,
      ShowUserModal,
      ShowVisitorModal
    },
    data () {
      return {
        description: '用户管理管理页面',
        // 表头
        columns: [
          {
            title: '#',
            dataIndex: '',
            key:'rowIndex',
            width:60,
            align:"center",
            customRender:function (t,r,index) {
              return parseInt(index)+1;
            }
          },
          {
            title:this.$t('所属APP'),
            align:"center",
            dataIndex: 'showAppId_dictText'
          },
          {
            title:this.$t('昵称'),
            align:"center",
            dataIndex: 'nickName'
          },
          {
            title:this.$t('姓名'),
            align:"center",
            dataIndex: 'realName'
          },
          {
            title:this.$t('性别'),
            align:"center",
            dataIndex: 'sex',
            scopedSlots: { customRender: 'customRenderSex' },
            filterMultiple: false
          },
          {
            title:this.$t('年代'),
            align:"center",
            dataIndex: 'years',
            scopedSlots: { customRender: 'customRenderYears' },
            filterMultiple: false
          },
          {
            title:this.$t('头像'),
            align:"center",
            width: 120,
            dataIndex: 'avatar',
            scopedSlots: {customRender: "avatarslot"}
          },
          {
            title:this.$t('手机号'),
            align:"center",
            dataIndex: 'phone'
          },
          {
            title:this.$t('详细地址'),
            align:"center",
            dataIndex: 'address'
          },
          {
            title:this.$t('邮箱'),
            align:"center",
            dataIndex: 'email'
          },
          {
            title:this.$t('会员权限'),
            align:"center",
            dataIndex: 'isAuth',
            scopedSlots: { customRender: 'customRenderAuth' },
            filterMultiple: false
          },
          {
            title:this.$t('注册时间'),
            align:"center",
            dataIndex: 'registerTime',
            customRender:function (text) {
              return !text?"":(text.length>10?text.substr(0,16):text)
            }
          },
          {
            title:this.$t('状态'),
            align:"center",
            dataIndex: 'state',
            scopedSlots: { customRender: 'customRenderStatus' },
            filterMultiple: false
          },
          {
            title:this.$t('操作'),
            dataIndex: 'action',
            align:"center",
            fixed:"right",
            width:147,
            scopedSlots: { customRender: 'action' }
          }
        ],
        url: {
          list: "/user/showUser/list",
          delete: "/user/showUser/delete",
          deleteBatch: "/user/showUser/deleteBatch",
          exportXlsUrl: "/user/showUser/exportXls",
          importExcelUrl: "user/showUser/importExcel",

        },
        dictOptions:{},
        superFieldList:[],
      }
    },
    created() {
    this.getSuperFieldList();
    },
    computed: {
      importExcelUrl: function(){
        return `${window._CONFIG['domianURL']}/${this.url.importExcelUrl}`;
      },
    },
    methods: {
      initDictConfig(){
      },
      getAvatarView: function (avatar) {
        return getFileAccessHttpUrl(avatar)
      },
      handleAuth: function (record) {
        this.$refs.authForm.open(record);
        this.$refs.authForm.title = this.$t("用户权限");
        this.$refs.authForm.disableSubmit = true;
      },
      batchOpen(){
        if (this.selectedRowKeys.length <= 0) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        }
        var ids = "";
        for (var a = 0; a < this.selectedRowKeys.length; a++) {
          ids += this.selectedRowKeys[a] + ",";
        }
        var that = this;
        httpAction("/user/showUser/batchOpen","ids="+ids,'post').then((res)=>{
          if(res.success){
            that.$message.success(res.message);
          }else{
            that.$message.warning(res.message);
          }
        }).finally(() => {
          that.reCalculatePage(that.selectedRowKeys.length)
          that.loadData();
          that.onClearSelected();
        })
      },
      batchClose(){
        if (this.selectedRowKeys.length <= 0) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        }
        var ids = "";
        for (var a = 0; a < this.selectedRowKeys.length; a++) {
          ids += this.selectedRowKeys[a] + ",";
        }
        var that = this;
        httpAction("/user/showUser/batchClose","ids="+ids,'post').then((res)=>{
          if(res.success){
            that.$message.success(res.message);
          }else{
            that.$message.warning(res.message);
          }
        }).finally(() => {
          that.reCalculatePage(that.selectedRowKeys.length)
          that.loadData();
          that.onClearSelected();
        })
      },
      batchEnable(){
        if (this.selectedRowKeys.length <= 0) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        }
        var ids = "";
        for (var a = 0; a < this.selectedRowKeys.length; a++) {
          ids += this.selectedRowKeys[a] + ",";
        }
        var that = this;
        httpAction("/user/showUser/batchEnable","ids="+ids,'post').then((res)=>{
          if(res.success){
            that.$message.success(res.message);
          }else{
            that.$message.warning(res.message);
          }
        }).finally(() => {
          that.reCalculatePage(that.selectedRowKeys.length)
          that.loadData();
          that.onClearSelected();
        })
      },
      batchDisable(){
        if (this.selectedRowKeys.length <= 0) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        }
        var ids = "";
        for (var a = 0; a < this.selectedRowKeys.length; a++) {
          ids += this.selectedRowKeys[a] + ",";
        }
        var that = this;
        httpAction("/user/showUser/batchDisable","ids="+ids,'post').then((res)=>{
          if(res.success){
            that.$message.success(res.message);
          }else{
            that.$message.warning(res.message);
          }
        }).finally(() => {
          that.reCalculatePage(that.selectedRowKeys.length)
          that.loadData();
          that.onClearSelected();
        })
      },
      handleVisitorModal: function () {
        this.$refs.visitorModal.open();
        this.$refs.visitorModal.title = this.$t("游客管理");
        this.$refs.visitorModal.disableSubmit = true;
      },
      getSuperFieldList(){
        let fieldList=[];
        this.superFieldList = fieldList
      }
    }
  }
</script>
<style scoped>
  @import '~@assets/less/common.less';
</style>