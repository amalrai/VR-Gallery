<template>
  <a-card :bordered="false">
    <!-- 查询区域 -->
    <div class="table-page-search-wrapper">
      <a-form layout="inline" @keyup.enter.native="searchQuery">
        <a-row :gutter="24">
          <a-col :md="6" :sm="8">
            <a-form-item :label="$t('账号')">
              <a-input :placeholder="$t('请输入账号查询')" v-model="queryParam.showUserEmail"></a-input>
            </a-form-item>
          </a-col>

          <a-col :md="12" :sm="16">
            <a-form-item :label="$t('登录时间')">
              <j-date :placeholder="$t('请选择开始时间')" class="query-group-cust" v-model="queryParam.loginTime_begin" show-time date-format="YYYY-MM-DD HH:mm:ss"></j-date>
              <span class="query-group-split-cust"></span>
              <j-date :placeholder="$t('请选择结束时间')" class="query-group-cust" v-model="queryParam.loginTime_end" show-time date-format="YYYY-MM-DD HH:mm:ss"></j-date>
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
<!--      <a-button type="primary" icon="download" @click="handleExportXls('登录日志')">导出</a-button>-->
<!--      <a-upload name="file" :showUploadList="false" :multiple="false" :headers="tokenHeader" :action="importExcelUrl" @change="handleImportExcel">-->
<!--        <a-button type="primary" icon="import">导入</a-button>-->
<!--      </a-upload>-->
<!--      &lt;!&ndash; 高级查询区域 &ndash;&gt;-->
<!--      <j-super-query :fieldList="superFieldList" ref="superQueryModal" @handleSuperQuery="handleSuperQuery"></j-super-query>-->
<!--      <a-dropdown v-if="selectedRowKeys.length > 0">-->
<!--        <a-menu slot="overlay">-->
<!--          <a-menu-item key="1" @click="batchDel"><a-icon type="delete"/>删除</a-menu-item>-->
<!--        </a-menu>-->
<!--        <a-button style="margin-left: 8px"> 批量操作 <a-icon type="down" /></a-button>-->
<!--      </a-dropdown>-->
    </div>

    <!-- table区域-begin -->
    <div>
<!--      <div class="ant-alert ant-alert-info" style="margin-bottom: 16px;">-->
<!--        <i class="anticon anticon-info-circle ant-alert-icon"></i> 已选择 <a style="font-weight: 600">{{ selectedRowKeys.length }}</a>项-->
<!--        <a style="margin-left: 24px" @click="onClearSelected">清空</a>-->
<!--      </div>-->

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
          <a @click="handleClickLog(record)">查看</a>
        </span>

      </a-table>
    </div>

    <show-user-click-log-list-modal ref="modalForm" @ok="modalFormOk"></show-user-click-log-list-modal>
  </a-card>
</template>

<script>

  import '@/assets/less/TableExpand.less'
  import { mixinDevice } from '@/utils/mixin'
  import { JeecgListMixin } from '@/mixins/JeecgListMixin'
  import ShowUserClickLogListModal from './modules/ShowUserClickLogListModal'

  export default {
    name: 'ShowUserLoginLogList',
    mixins:[JeecgListMixin, mixinDevice],
    components: {
      ShowUserClickLogListModal
    },
    data () {
      return {
        description: '登录日志管理页面',
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
            title:this.$t('IP'),
            align:"center",
            dataIndex: 'loginIp'
          },
          {
            title:this.$t('账号'),
            align:"center",
            dataIndex: 'showUserEmail'
          },
          {
            title:this.$t('房间'),
            align:"center",
            dataIndex: 'showRoomId_dictText'
          },
          {
            title:this.$t('登录时间'),
            align:"center",
            dataIndex: 'loginTime',
            customRender:function (text) {
              return !text?"":(text.length>19?text.substr(0,19):text)
            }
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
        url: {
          list: "/log/showUserLoginLog/list",
          delete: "/log/showUserLoginLog/delete",
          deleteBatch: "/log/showUserLoginLog/deleteBatch",
          exportXlsUrl: "/log/showUserLoginLog/exportXls",
          importExcelUrl: "log/showUserLoginLog/importExcel",

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
      handleClickLog: function (record) {
        this.$refs.modalForm.open(record);
        this.$refs.modalForm.title = this.$t("操作日志");
        this.$refs.modalForm.disableSubmit = true;
      },
      initDictConfig(){
      },
      getSuperFieldList(){
        let fieldList=[];
        fieldList.push({type:'string',value:'showUserId',text:'用户ID'})
        fieldList.push({type:'string',value:'showUserNickName',text:'用户昵称'})
        fieldList.push({type:'string',value:'showUserRealName',text:'用户姓名'})
        fieldList.push({type:'string',value:'showUserEmail',text:'用户账号'})
        fieldList.push({type:'string',value:'showMuseumId',text:'登录展馆ID'})
        fieldList.push({type:'string',value:'showRoomId',text:'登录房间ID'})
        fieldList.push({type:'string',value:'loginIp',text:'登录ip'})
        fieldList.push({type:'date',value:'loginTime',text:'登录时间'})
        this.superFieldList = fieldList
      }
    }
  }
</script>
<style scoped>
  @import '~@assets/less/common.less';
</style>