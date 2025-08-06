<template>
  <a-card :bordered="false">
    <!-- 查询区域 -->
    <div class="table-page-search-wrapper">
      <a-form layout="inline" @keyup.enter.native="searchQuery">
        <a-row :gutter="24">
        </a-row>
      </a-form>
    </div>
    <!-- 查询区域-END -->

    <!-- 操作按钮区域 -->
    <div class="table-operator">
      <a-button @click="handleAdd" type="primary" icon="plus">{{$t('新增')}}</a-button>
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
        <i class="anticon anticon-info-circle ant-alert-icon"></i> {{ $t('已选择') }} <a style="font-weight: 600">{{ selectedRowKeys.length }}</a>{{$t('项')}}
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
          <a @click="handleDetail(record)">{{$t('详情')}}</a>
          <a-divider type="vertical" />
          <a @click="handleEdit(record)">{{$t('编辑')}}</a>

          <a-divider type="vertical" />
          <a-popconfirm :title="$t('确定删除吗?')" @confirm="() => handleDelete(record.id)" :okText="$t('确定')" :cancelText="$t('取消')">
                            <a>{{$t('删除')}}</a>
                          </a-popconfirm>
        </span>

      </a-table>
    </div>

    <show-app-modal ref="modalForm" @ok="modalFormOk"></show-app-modal>
  </a-card>
</template>

<script>

  import '@/assets/less/TableExpand.less'
  import { mixinDevice } from '@/utils/mixin'
  import { JeecgListMixin } from '@/mixins/JeecgListMixin'
  import ShowAppModal from './modules/ShowAppModal'

  export default {
    name: 'ShowAppList',
    mixins:[JeecgListMixin, mixinDevice],
    components: {
      ShowAppModal
    },
    data () {
      return {
        description: 'APP管理',
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
            title:this.$t('APPID'),
            align:"center",
            dataIndex: 'id'
          },
          {
            title:this.$t('APP名称'),
            align:"center",
            dataIndex: 'name'
          },
          {
            title:this.$t('APP描述'),
            align:"center",
            dataIndex: 'description'
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
          list: "/show/showApp/list",
          delete: "/show/showApp/delete",
          deleteBatch: "/show/showApp/deleteBatch",
          exportXlsUrl: "/show/showApp/exportXls",
          importExcelUrl: "show/showApp/importExcel",

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
      getSuperFieldList(){
        let fieldList=[];
        fieldList.push({type:'string',value:'name',text:'APP名称'})
        fieldList.push({type:'string',value:'description',text:'APP描述'})
        this.superFieldList = fieldList
      }
    }
  }
</script>
<style scoped>
  @import '~@assets/less/common.less';
</style>