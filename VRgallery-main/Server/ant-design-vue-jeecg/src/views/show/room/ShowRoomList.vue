<template>
  <a-card :bordered="false">
    <!-- 查询区域 -->
    <div class="table-page-search-wrapper">
      <a-form layout="inline" @keyup.enter.native="searchQuery">
        <a-row :gutter="24">
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('房间名')">
              <j-input :placeholder="$t('输入房间名模糊查询')" v-model="queryParam.name"></j-input>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="12">
            <a-form-item :label="$t('房间编号')">
              <j-input :placeholder="$t('输入房间编号查询')" v-model="queryParam.roomNo" type=""></j-input>
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
        <template slot="customRenderFreeFlag" slot-scope="freeFlag">
          <a-tag v-if="freeFlag==0" color="orange">{{$t('VIP')}}</a-tag>
          <a-tag v-if="freeFlag==1" color="green">{{$t('免费')}}</a-tag>
        </template>
        <span slot="action" slot-scope="text, record">
          <a @click="handleExhibitsList(record)">{{$t('展品管理')}}</a>

          <a-divider type="vertical" />
          <a @click="handleEdit(record)">{{$t('编辑')}}</a>

          <a-divider type="vertical" />
          <a-popconfirm :title="$t('确定删除吗?')" @confirm="() => handleDelete(record.id)" :okText="$t('确定')" :cancelText="$t('取消')">
                            <a>{{$t('删除')}}</a>
                          </a-popconfirm>
        </span>

      </a-table>
    </div>

    <show-room-modal ref="modalForm" @ok="modalFormOk"></show-room-modal>
    <show-exhibits-list-modal ref="exhibitsListForm" @ok="modalFormOk"></show-exhibits-list-modal>
  </a-card>
</template>

<script>

  import '@/assets/less/TableExpand.less'
  import { mixinDevice } from '@/utils/mixin'
  import { JeecgListMixin } from '@/mixins/JeecgListMixin'
  import ShowRoomModal from './modules/ShowRoomModal'
  import ShowExhibitsListModal from './modules/ShowExhibitsListModal'

  export default {
    name: 'ShowRoomList',
    mixins:[JeecgListMixin, mixinDevice],
    components: {
      ShowRoomModal,
      ShowExhibitsListModal
    },
    data () {
      return {
        description: 'show_room管理页面',
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
            title:this.$t('房间名'),
            align:"center",
            dataIndex: 'name'
          },
          {
            title:this.$t('房间编号'),
            align:"center",
            dataIndex: 'roomNo'
          },
          {
            title:this.$t('是否免费'),
            align:"center",
            dataIndex: 'freeFlag',
            scopedSlots: { customRender: 'customRenderFreeFlag' }
          },
          {
            title:this.$t('说明'),
            align:"center",
            dataIndex: 'description'
          },
          {
            title: this.$t('展馆'),
            align: "center",
            width: 100,
            dataIndex: 'showMuseumId_dictText'
          },
          {
            title:this.$t('创建时间'),
            align:"center",
            dataIndex: 'createTime'
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
          list: "/room/showRoom/list",
          delete: "/room/showRoom/delete",
          deleteBatch: "/room/showRoom/deleteBatch",
          exportXlsUrl: "/room/showRoom/exportXls",
          importExcelUrl: "room/showRoom/importExcel",

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
      handleExhibitsList: function (record) {
        this.$refs.exhibitsListForm.open(record);
        this.$refs.exhibitsListForm.title = this.$t("展品管理");
        this.$refs.exhibitsListForm.disableSubmit = true;
      },
      getSuperFieldList(){
        let fieldList=[];
        fieldList.push({type:'string',value:'name',text:'房间'})
        fieldList.push({type:'int',value:'roomNo',text:'房间编号'})
        fieldList.push({type:'string',value:'description',text:'说明'})
        fieldList.push({type:'string',value:'musicName',text:'音频名'})
        fieldList.push({type:'string',value:'musicSize',text:'音频大小'})
        fieldList.push({type:'string',value:'musicUrl',text:'音频地址'})
        this.superFieldList = fieldList
      }
    }
  }
</script>
<style scoped>
  @import '~@assets/less/common.less';
</style>