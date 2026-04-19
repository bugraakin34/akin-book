import { useEffect, useState } from "react";
import type { Book } from "../types/book";
import { deleteBook, getBooks } from "../services/bookService";
import { getMe } from "../services/authService";
import { Button, Input, message, Space, Table, Typography } from "antd";
import type { ColumnsType } from "antd/es/table";
import { useTranslation } from "react-i18next";
import LanguageSwitcher from "../components/LanguageSwitcher";

export default function BooksPage() {
  const [items, setItems] = useState<Book[]>([]);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [totalCount, setTotalCount] = useState(0);
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(false);
  const [role, setRole] = useState("");
  const { t, i18n } = useTranslation();

  const loadData = async () => {
    try {
      setLoading(true);
      const [booksData, me] = await Promise.all([
        getBooks({ page, pageSize, search }),
        getMe(),
      ]);

      setItems(booksData.items);
      setTotalCount(booksData.totalCount);
      setRole(me.role);
    } catch {
      message.error(t("books.loadError"));
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, [page, pageSize, search]);

  const handleDelete = async (id: string) => {
    try {
      await deleteBook(id);
      message.success(t("books.deleteSuccess"));
      loadData();
    } catch {
      message.error(t("books.deleteError"));
    }
  };

  const columns: ColumnsType<Book> = [
    {
      title: t("books.bookTitle"),
      render: (_, record) =>
        i18n.language.startsWith("tr") ? record.titleTr : record.titleEn,
    },
    { title: t("books.author"), dataIndex: "author" },
    {
      title: t("books.description"),
      render: (_, record) =>
        i18n.language.startsWith("tr") ? record.descriptionTr : record.descriptionEn,
    },
    { title: t("books.year"), dataIndex: "publishedYear" },
    {
      title: t("books.actions"),
      render: (_, record) =>
        role === "Admin" ? (
          <Space>
            <Button danger onClick={() => handleDelete(record.id)}>
              {t("books.delete")}
            </Button>
          </Space>
        ) : null,
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <Space
        style={{
          width: "100%",
          justifyContent: "space-between",
          marginBottom: 16,
        }}
      >
        <Typography.Title level={2} style={{ margin: 0 }}>
          {t("books.title")}
        </Typography.Title>

        <LanguageSwitcher />
      </Space>

      <Space style={{ marginBottom: 16 }}>
        <Input
          placeholder={t("books.searchPlaceholder")}
          value={search}
          onChange={(e) => {
            setPage(1);
            setSearch(e.target.value);
          }}
          style={{ width: 240 }}
        />
      </Space>

      <Table
        rowKey="id"
        loading={loading}
        columns={columns}
        dataSource={items}
        pagination={{
          current: page,
          pageSize,
          total: totalCount,
          onChange: (nextPage, nextPageSize) => {
            setPage(nextPage);
            setPageSize(nextPageSize);
          },
        }}
      />
    </div>
  );
}
