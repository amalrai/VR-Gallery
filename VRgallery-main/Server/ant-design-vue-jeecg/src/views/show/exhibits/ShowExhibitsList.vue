<template>
  <a-card :bordered="false">
    <!-- 查询区域 -->
    <div class="table-page-search-wrapper">
      <a-form layout="inline" @keyup.enter.native="searchQuery">
        <a-row :gutter="24">
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('展品名')">
              <j-input :placeholder="$t('输入展品名模糊查询')" v-model="queryParam.name"></j-input>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('编号')">
              <j-input :placeholder="$t('输入编号查询')" v-model="queryParam.exhibitsNo" type=""></j-input>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('状态')">
              <a-select :placeholder="$t('选择状态查询')" v-model="queryParam.status" >
                          <a-select-option value="">{{$t('请选择')}}</a-select-option>
                          <a-select-option value="0">{{$t('启用')}}</a-select-option>
                          <a-select-option value="1">{{$t('禁用')}}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
            <a-col :md="6" :sm="12">
            <a-form-item :label="$t('权限')">
              <a-select :placeholder="$t('选择权限查询')" v-model="queryParam.auth" >
                                        <a-select-option value="">{{$t('请选择')}}</a-select-option>
                                        <a-select-option value="0">{{$t('免费')}}</a-select-option>
                                        <a-select-option value="1">{{$t('VIP')}}</a-select-option>
                            </a-select>
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
      <a-button @click="handleAddExhibits" type="primary" icon="plus">{{$t('新增')}}</a-button>
      <a-button @click="batchEnable" type="primary" >{{$t('启用')}}</a-button>
      <a-button @click="batchDisable" type="primary" >{{$t('禁用')}}</a-button>
      <a-button @click="batchCharge" type="primary" >{{$t('VIP')}}</a-button>
      <a-button @click="batchFree" type="primary" >{{$t('免费')}}</a-button>
      <a-button @click="handleAddRoomBgm" type="primary" icon="customer-service">{{$t('上传BGM')}}</a-button>
      <a-button @click="handleAddRoomResource" type="primary" icon="folder-add">{{$t('上传资源包')}}</a-button>
<!--      <a-button @click="handleAddRoomResourceScene" type="primary" icon="folder-add">{{$t('上传场景包')}}</a-button>-->
      <a-dropdown v-if="selectedRowKeys.length > 0">
        <a-menu slot="overlay">
          <a-menu-item key="1" @click="batchClear"><a-icon type="delete"/>{{$t('清空')}}</a-menu-item>
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
          <a @click="handleEdit(record)">{{$t('编辑')}}</a>
          <a-divider type="vertical" />
          <a-popconfirm :title="$t('确定清空吗?')" :okText="$t('确定')" :cancelText="$t('取消')" @confirm="() => handleClear(record.id)">
                            <a>{{$t('清空')}}</a>
                          </a-popconfirm>
          <a-divider type="vertical" />
          <a @click="handleReplaceExhibits(record)">{{$t('替换')}}</a>
        </span>
        <template slot="customRenderStatus" slot-scope="status">
                  <a-tag v-if="status==0" color="green">{{$t('启用')}}</a-tag>
                  <a-tag v-if="status==1" color="red">{{$t('禁用')}}</a-tag>
        </template>
        <template slot="customRenderAuth" slot-scope="auth">
                                  <a-tag v-if="auth==0" color="green">{{$t('免费')}}</a-tag>
                                  <a-tag v-if="auth==1" color="orange">{{$t('VIP')}}</a-tag>
                </template>
      </a-table>
    </div>

    <show-exhibits-modal ref="modalForm" @ok="modalFormOk"></show-exhibits-modal>
    <show-exhibits-add-modal ref="modalAddForm" @ok="modalFormOk"></show-exhibits-add-modal>
    <show-exhibits-list-modal ref="modalListForm" @ok="modalFormOk"></show-exhibits-list-modal>
    <show-room-bgm-modal ref="modalRoomBgmForm" @ok="modalFormOk"></show-room-bgm-modal>
    <show-room-resource-modal ref="modalRoomResourceForm" @ok="modalFormOk"></show-room-resource-modal>
    <show-room-resource-scene-modal ref="modalRoomResourceSceneForm" @ok="modalFormOk"></show-room-resource-scene-modal>
  </a-card>
</template>

<script>

  import '@/assets/less/TableExpand.less'
  import { mixinDevice } from '@/utils/mixin'
  import { JeecgListMixin } from '@/mixins/JeecgListMixin'
  import ShowExhibitsModal from './modules/ShowExhibitsModal'
  import ShowExhibitsAddModal from './modules/ShowExhibitsAddModal'
  import ShowExhibitsListModal from './modules/ShowExhibitsListModal'
  import ShowRoomBgmModal from './../room/modules/ShowRoomBgmModal'
  import ShowRoomResourceModal from './../room/modules/ShowRoomResourceModal'
  import ShowRoomResourceSceneModal from './../room/modules/ShowRoomResourceSceneModal'
  import {deleteAction} from "@api/manage";
  import {httpAction} from "@api/manage";
  export default {
    name: 'ShowExhibitsList',
    mixins:[JeecgListMixin, mixinDevice],
    components: {
      ShowExhibitsModal,
      ShowExhibitsAddModal,
      ShowExhibitsListModal,
      ShowRoomBgmModal,
      ShowRoomResourceModal,
      ShowRoomResourceSceneModal
    },
    data () {
      return {
        description: '展品管理管理页面',
        //房间ID
        roomId:'',
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
            title:this.$t('编号'),
            align:"center",
            dataIndex: 'exhibitsNo'
          },
          {
            title:this.$t('展品名'),
            align:"center",
            dataIndex: 'name'
          },
          {
            title:this.$t('文字简介'),
            align:"center",
            dataIndex: 'text'
          },
          {
            title:this.$t('状态'),
            align:"center",
            dataIndex: 'status',
            scopedSlots: { customRender: 'customRenderStatus' },
            filterMultiple: false
          },
          {
            title:this.$t('权限'),
            align:"center",
            dataIndex: 'auth',
            scopedSlots: { customRender: 'customRenderAuth' }
          },
          {
            title: this.$t('操作'),
            dataIndex: 'action',
            align:"center",
            fixed:"right",
            width:147,
            scopedSlots: { customRender: 'action' }
          }
        ],
        /* 排序参数 */
        isorter:{
          column: 'exhibitsNo',
          order: 'asc',
        },
        url: {
          list: "/exhibits/showExhibits/list",
          batchUpdateStatus: "/exhibits/showExhibits/batchUpdateStatus",
          batchUpdateAuth: "/exhibits/showExhibits/batchUpdateAuth",
          batchClear: "/exhibits/showExhibits/batchClear",
          clear: "/exhibits/showExhibits/clear",
          delete: "/exhibits/showExhibits/delete",
          deleteBatch: "/exhibits/showExhibits/deleteBatch",
          exportXlsUrl: "/exhibits/showExhibits/exportXls",
          importExcelUrl: "exhibits/showExhibits/importExcel",

        },
        disableMixinCreated:true,
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
      getSuperFieldList(){},
      open(record){
        this.roomId = record.id;
        this.queryParam.showRoomId = record.id;
        this.searchQuery();
      },
      searchReset() {
        this.queryParam = {}
        this.queryParam.showRoomId = this.roomId;
        this.searchQuery();
      },
      handleAddExhibits(){
        this.$refs.modalAddForm.add(this.roomId);
        this.$refs.modalAddForm.title = this.$t("新增");
        this.$refs.modalAddForm.disableSubmit = false;
      },
      handleReplaceExhibits(record){
        this.$refs.modalListForm.open(record);
        this.$refs.modalListForm.title = this.$t("替换");
        this.$refs.modalListForm.disableSubmit = false;
      },
      handleAddRoomBgm(){
        this.$refs.modalRoomBgmForm.edit(this.roomId);
        this.$refs.modalRoomBgmForm.title = this.$t("上传BGM");
        this.$refs.modalRoomBgmForm.disableSubmit = false;
      },
      handleAddRoomResource(){
        this.$refs.modalRoomResourceForm.edit(this.roomId);
        this.$refs.modalRoomResourceForm.title = this.$t("上传资源包");
        this.$refs.modalRoomResourceForm.disableSubmit = false;
      },
      handleAddRoomResourceScene(){
        this.$refs.modalRoomResourceSceneForm.edit(this.roomId);
        this.$refs.modalRoomResourceSceneForm.title = this.$t("上传场景包");
        this.$refs.modalRoomResourceSceneForm.disableSubmit = false;
      },
      batchEnable(){
        this.batchUpdateStatus(0);
      },
      batchDisable(){
        this.batchUpdateStatus(1);
      },
      batchCharge(){
        this.batchUpdateAuth(1);
      },
      batchFree(){
        this.batchUpdateAuth(0);
      },
      batchUpdateStatus(status){
        if (this.selectedRowKeys.length <= 0) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        }
        var ids = "";
        for (var a = 0; a < this.selectedRowKeys.length; a++) {
          ids += this.selectedRowKeys[a] + ",";
        }
        var that = this;
        deleteAction(this.url.batchUpdateStatus,{ids: ids,status:status}).then((res)=>{
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
      batchUpdateAuth(auth){
        if (this.selectedRowKeys.length <= 0) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        }
        var ids = "";
        for (var a = 0; a < this.selectedRowKeys.length; a++) {
          ids += this.selectedRowKeys[a] + ",";
        }
        var that = this;
        deleteAction(this.url.batchUpdateAuth,{ids: ids,auth:auth}).then((res)=>{
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
      batchClear: function () {
        if (this.selectedRowKeys.length <= 0) {
          this.$message.warning(this.$t('请选择一条记录！'));
          return;
        } else {
          var ids = "";
          for (var a = 0; a < this.selectedRowKeys.length; a++) {
            ids += this.selectedRowKeys[a] + ",";
          }
          var that = this;
          this.$confirm({
            title: this.$t("确认清空"),
            content: this.$t("是否清空选中数据?"),
            okText:this.$t('确定'),
            cancelText:this.$t('取消'),
            onOk: function () {
              that.loading = true;
              deleteAction(that.url.batchClear, {ids: ids}).then((res) => {
                if (res.success) {
                  //重新计算分页问题
                  that.reCalculatePage(that.selectedRowKeys.length)
                  that.$message.success(res.message);
                  that.loadData();
                  that.onClearSelected();
                } else {
                  that.$message.warning(res.message);
                }
              }).finally(() => {
                that.loading = false;
              });
            }
          });
        }
      },
      handleClear: function (id) {
        var that = this;
        deleteAction(that.url.clear, {id: id}).then((res) => {
          if (res.success) {
            //重新计算分页问题
            that.reCalculatePage(1)
            that.$message.success(res.message);
            that.loadData();
          } else {
            that.$message.warning(res.message);
          }
        });
      },
    }
  }
</script>
<style scoped>
  @import '~@assets/less/common.less';
</style>